        using System;
        using _TevLib.CoreLib;
        using _TevLib.ModuleSystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Agents
{
    public class AgentJumper : MonoModule , IJumpable , IManyJumpable
    {
        [field:SerializeField] public Rigidbody2D RbCompo {get; private set;}
        [field:Header("Jump Settings")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public bool ResetVelocity = true;
        [SerializeField] private float defaultJumpForce = 5f;
        
        [field:Header("Double Jump Settings")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public bool CanManyJump { get; private set; }
        
        [field:ShowIf("CanManyJump")]
        [field: SerializeField] public int JumpCount {get; private set;}
        public NotifyValue<int> _CurrentJumpCount { get; private set; } = new();
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnJumpEvent {get; private set;}

        public bool CanJump { get; private set; } = true;

        private void Awake() 
            => ResetCrtJump();

        public void Jump() 
            => JumpWithForce(defaultJumpForce);

        public void JumpWithForce(float force)
        {
            if (!CanJump) return;

            switch (CanManyJump)
            {
                case true when _CurrentJumpCount.Value <= 0: return;
                case true:
                    _CurrentJumpCount.Value--;
                    break;
            }

            JumpLogic(force);
        }

        public void SetCanJump(bool value)
            => CanJump = value;
        
        public void ResetCrtJump() 
            => _CurrentJumpCount.Value = JumpCount;
        
        private void JumpLogic(float force)
        {
            if (ResetVelocity)
                RbCompo.linearVelocityY = 0f;
            RbCompo.AddForceY(force, ForceMode2D.Impulse);
        }

        #region ManyJump

        public void SetJumpCount(int value) 
            => JumpCount = value;
        
        #endregion
    }

    public interface IManyJumpable
    {
        bool CanManyJump { get; }
        int JumpCount {get; }
        NotifyValue<int> _CurrentJumpCount { get;}
        
        void ResetCrtJump();
        void SetCanJump(bool value);
    }
}