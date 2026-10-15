using System;
using _TevLib.CoreLib;
using _TevLib.ModuleSystem;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Agents
{
    public class AgentGroundChecker : MonoModule , IGroundChecker
    {
        [field:Header("Checker Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field: SerializeField] public bool CheckGround { get; private set; } = true;
        [SerializeField] private LayerMask whatIsGround;
        
        [Header("Overlap Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private Vector2 overlapSize;
        
        [field:SerializeField] public NotifyValue<bool> IsGrounded { get; private set;} = new();

        private void Update()
        {
            if (!CheckGround) return;
            IsGrounded.Value = GroundCheck();
        }

        private bool GroundCheck()
        {
            Collider2D groundColl = Physics2D.OverlapBox(transform.position,overlapSize,0,whatIsGround);
            return groundColl != null;
        }
        
        #if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, overlapSize);
        }

#endif
    }
}