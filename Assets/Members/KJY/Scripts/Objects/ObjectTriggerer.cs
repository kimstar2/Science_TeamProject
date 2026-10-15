using System;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public class ObjectTriggerer : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITriggerable triggerable))
                triggerable.EnterTrigger();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITriggerable triggerable))
                triggerable.ExitTrigger();
        }
    }
}