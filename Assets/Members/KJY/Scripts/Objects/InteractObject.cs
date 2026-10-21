using System;
using UnityEngine;
using UnityEngine.Events;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Objects
{
    public class InteractObject : MonoBehaviour, IInteractable
    {
        public Collider2D Collider { get; private set; }
        
        [Header("Event")]
        public UnityEvent onInteractEnter;
        public UnityEvent onInteractExit;
        public UnityEvent onEnter;
        public UnityEvent onExit;
        public bool IsEntered { get; private set; }
        public bool IsInteracted { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }

        public void Interact()
        {
            if (!IsEntered) return;
            IsInteracted = !IsInteracted;
            if (IsInteracted)
                onInteractEnter?.Invoke();
            else
                onInteractExit?.Invoke();
        }

        public void EnterTrigger()
        {
            IsEntered = true;
            onEnter?.Invoke();
        }

        public void ExitTrigger()
        {
            IsEntered = false;
            onExit?.Invoke();
        }
    }
}