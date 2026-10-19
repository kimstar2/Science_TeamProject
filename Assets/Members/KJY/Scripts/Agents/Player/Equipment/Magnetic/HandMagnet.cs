using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Objects;
using Members.KJY.Scripts.Objects.Overlap;
using Members.KJY.Scripts.Utils;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using ZLinq;

namespace Members.KJY.Scripts.Agents.Player.Equipment.Magnetic
{
    public class HandMagnet : CircleCaster , IMagnet , IModule
    {
        [field:SerializeField] public MagnetState CurrentState { get; private set; }
        
        [field:Header("Magnet Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public Transform MagnetPivot { get; private set; }
        [SerializeField] private float defaultPullForce;
        [SerializeField] private float defaultPushForce;
        
        [field: Header("Event Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [SerializeField] public UnityEvent<bool> onPullEvent;
        [SerializeField] public UnityEvent<bool> onPushEvent;
        [SerializeField] public UnityEvent onNoneEvent;
        
        private Vector2 _direction;
        private float _pullForce;
        private float _pushForce;

        public bool IsClicked { get; private set; }

        protected override void Init()
        {
            base.Init();
            _pullForce = defaultPullForce;
            _pushForce = defaultPushForce;
        }

        public void SetDirection(Vector2 worldPos)
        {
            Vector2 dir = (Vector3)worldPos - transform.position;
            _direction = dir.normalized;
        }

        public void SetState(MagnetState state, bool isClicked)
        {
            if (CurrentState != state && GetClicked(CurrentState)) return;

            IsClicked = isClicked;
            CurrentState = !isClicked ? MagnetState.None : state;
            
            switch (state)
            {
                case MagnetState.Pull:
                    Pull(isClicked);
                    break;
                case MagnetState.Push:
                    Push(isClicked);
                    break;
            }
        }

        private void Pull(bool isClicked)
        {
            if (isClicked)
                onPullEvent?.Invoke(true);
            else
                onNoneEvent?.Invoke();
        }

        private void Push(bool isClicked)
        {
            if (isClicked)
                onPushEvent?.Invoke(true);
            else
                onNoneEvent?.Invoke();
        }

        #region Helper

        public override void Cast()
        {
            base.Cast();
            for (int i = 0; i < Count; i++)
            {
                if (results[i].TryGetComponent(out IMagnetable magnetable))
                    switch (CurrentState)
                    {
                        case MagnetState.Pull:
                            magnetable.Pull(this, _pullForce);
                            break;
                        case MagnetState.Push:
                            magnetable.Push(transform.position,_direction, _pushForce);
                            break;
                    }
            }   
        }
        
        public bool GetClicked(MagnetState state) => CurrentState == state && IsClicked;

        #endregion

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(targetTransform.position, radius);
        }

#endif
    }
}