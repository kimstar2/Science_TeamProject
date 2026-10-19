using System;
using NaughtyAttributes;
using UnityEngine;
using ZLinq;

namespace Members.KJY.Scripts.Objects.Overlap
{
    public class BoxCaster : OverlapCaster
    {
        [Header("Overlap Setting")] 
        [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private Color gizmoColor = Color.red;

        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;

        public override void Cast()
        {
            Count = Physics2D.OverlapBox(targetTransform.position + (Vector3)offset, size,0, contactFilter, results);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (targetTransform == null) return;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(targetTransform.position + (Vector3)offset, size);
        }

#endif
    }
}