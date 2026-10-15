using System;
using _TevLib.Extension.DoT;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Objects
{
    public class ButtonObject : MonoBehaviour , ITriggerable
    {
        [Header("Dest Setting")]
        [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private Vector3 moveLocal;
        [SerializeField] private TweenStep step;

        [Header("Button Setting")] [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private bool autoExit;
        [SerializeField] private UnityEvent onButton;
        [SerializeField] private UnityEvent offButton;
        public void EnterTrigger() 
            => OnButton();

        private void OnButton()
        {
            transform.DOKill();
            transform.DOLocalMove(moveLocal, step.Duration).
                SetEase(step.EaseType).OnComplete(autoExit ? OffButton : () => {});
            onButton?.Invoke();
        }

        public void ExitTrigger() { }

        private void OffButton()
        {
            transform.DOKill();
            transform.DOLocalMove(Vector3.zero, step.Duration).SetEase(step.EaseType);
            offButton?.Invoke();
        }

        private void OnDestroy() 
            => transform.DOKill();
    }
}
