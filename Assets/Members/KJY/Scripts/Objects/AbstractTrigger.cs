using System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Objects
{
    public abstract class AbstractTrigger : MonoBehaviour , ITriggerable
    {
        public bool IsEntered { get; protected set; }
        public Collider2D Collider { get; protected set; }
        public event Action OnTrigger;

        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }

        public virtual void EnterTrigger()
        {
            IsEntered = true;
            OnTrigger?.Invoke();
        }

        public virtual void ExitTrigger()
        {
            IsEntered = false;
            OnTrigger?.Invoke();
        }
    }
}