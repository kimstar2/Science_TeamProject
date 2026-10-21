    using System;
using _TevLib.Extension.DoT;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Objects
{
    public class ButtonObject : AbstractTrigger
    {
        [SerializeField] private Transform targetTransform;
        [Header("Dest Setting")]
        [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private Vector3 moveLocal;
        [SerializeField] private TweenStep step;

        [Header("Button Setting")] [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private bool useExit;
        [SerializeField] private bool autoExit;
        [SerializeField] private UnityEvent onButton;
        [SerializeField] private UnityEvent offButton;
        public override void EnterTrigger()
        {
            base.EnterTrigger();
            OnButton();
        }

        private void OnButton()
        {
            targetTransform.DOKill();
            targetTransform.DOLocalMove(moveLocal, step.Duration).
                SetEase(step.EaseType).OnComplete(autoExit && useExit ? OffButton : () => {});
            onButton?.Invoke();
        }

        public override void ExitTrigger()
        {
            base.ExitTrigger();
            if (!useExit) return;
            OffButton();
        }

        private void OffButton()
        {
            targetTransform.DOKill();
            targetTransform.DOLocalMove(Vector3.zero, step.Duration).SetEase(step.EaseType);
            offButton?.Invoke();
        }

        private void OnDestroy() 
            => targetTransform.DOKill();
        
        public event Action<ITriggerable> OnExit;
        private void OnTriggerExit2D(Collider2D other) 
            => OnExit?.Invoke(this);

        private void OnCollisionEnter2D(Collision2D other)
            => OnExit?.Invoke(this);


    }
}
