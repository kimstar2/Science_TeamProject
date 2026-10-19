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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.callbackLayers.value & targetLayer.value) == 0) return;
            
            if (other.TryGetComponent(out ITriggerable trigger))
                trigger.EnterTrigger();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if ((other.callbackLayers.value & targetLayer.value) == 0) return;
            
            if (other.TryGetComponent(out ITriggerable trigger))
                trigger.ExitTrigger();
        }
    }
}