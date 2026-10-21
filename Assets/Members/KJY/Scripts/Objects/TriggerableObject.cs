using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Objects
{
    public class TriggerableObject : MonoBehaviour , ITriggerable
    {
        public Collider2D Collider { get; private set; }
        
        [Header("Event Setting")] 
        [HorizontalLine(color: EColor.Gray)] 
        public UnityEvent onEnter;
        public UnityEvent onExit;
        public bool IsEntered { get; private set; }


        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
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