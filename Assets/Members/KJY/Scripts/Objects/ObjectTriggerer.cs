using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Objects.Overlap;
using NaughtyAttributes;
using UnityEngine;
using ZLinq;

namespace Members.KJY.Scripts.Objects
{
    public class ObjectTriggerer : MonoModule
    {
        [Header("Layer Settings")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private LayerMask targetLayer;

        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public event Action<ITriggerable> OnTriggerEnter;
        public event Action<ITriggerable> OnTriggerExit;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.callbackLayers.value & targetLayer.value) == 0) return;
            if (!other.TryGetComponent(out ITriggerable trigger)) return;
            if (trigger.Collider.transform == _collider.transform) return;
            OnTriggerEnter?.Invoke(trigger);
            trigger.EnterTrigger();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if ((other.callbackLayers.value & targetLayer.value) == 0) return;
            if (!other.TryGetComponent(out ITriggerable trigger)) return;
            if (trigger.Collider.transform == _collider.transform) return;
            
            OnTriggerExit?.Invoke(trigger);
            trigger.ExitTrigger();
        }
    }
}