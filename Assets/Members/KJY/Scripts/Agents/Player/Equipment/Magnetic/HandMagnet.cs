using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Objects;
using Members.KJY.Scripts.Utils;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using ZLinq;

namespace Members.KJY.Scripts.Agents.Player.Equipment.Magnetic
{
    public class HandMagnet : MonoModule
    {
        [field:SerializeField] public MagnetState CurrentState { get; private set; }
        
        [field:Header("Target Setting")]
        [field:HorizontalLine(color:EColor.Gray)] // <- 이거는 그냥 인스펙터에 선 띄워주는 용도
        [field:SerializeField] public ContactFilter2D ContactFilter { get; private set; }
        [SerializeField] private int defaultCount;
        private Collider2D[] _results;
        private int _count;
        
        [field:Header("Magnet Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public Transform MagnetPivot { get; private set; }
        [SerializeField] private float defaultPullForce;
        [SerializeField] private float defaultPushForce;
        [SerializeField] private float radius = 0.5f;
        
        [field: Header("Event Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [SerializeField] public UnityEvent<bool> onPullEvent;
        [SerializeField] public UnityEvent<bool> onPushEvent;
        [SerializeField] public UnityEvent onNoneEvent;
        
        
        private readonly List<IMagnetable> _magnetableList = new();
        
        private Vector2 _direction;
        private float _pullForce;
        private float _pushForce;

        public bool IsClicked { get; private set; }
        
        public override void Init(ModuleOwner owner)
        {
            base.Init(owner);
            _pullForce = defaultPullForce;
            _pushForce = defaultPushForce;
            _count = defaultCount;
            _results = new Collider2D[defaultCount];
        }

        private void Update()
        {
            DetectMagnetable();
            foreach (var magnetable in _magnetableList)
            {
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

        private void DetectMagnetable()
        {
            _magnetableList.Clear();
            for (int i = 0; i < DetectObjects(); i++)
            {
                if (_results[i].TryGetComponent(out IMagnetable magnetable))
                    _magnetableList.Add(magnetable);
            }
        }

        private int DetectObjects()
        {
            _count = Physics2D.OverlapCircle(MagnetPivot.position, radius, ContactFilter, _results);
            return _count;
        }

        public bool GetClicked(MagnetState state) => CurrentState == state && IsClicked;

        #endregion

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(MagnetPivot.position, radius);
        }

#endif
    }
}