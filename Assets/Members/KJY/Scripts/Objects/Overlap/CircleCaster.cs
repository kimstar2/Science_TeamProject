using System;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Objects.Overlap
{
    public class CircleCaster : OverlapCaster
    {
        [Header("Overlap Setting")] 
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] protected Color gizmoColor = Color.red;
        [SerializeField] protected float radius;
        [SerializeField] protected Vector2 offset;

        public override void Cast()
        {
            Count = Physics2D.OverlapCircle(targetTransform.position + (Vector3)offset, radius, contactFilter, results);
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (targetTransform == null) return;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(targetTransform.position + (Vector3)offset, radius);
        }

#endif
    }
}