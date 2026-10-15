        using _TevLib.ModuleSystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Agents
{
    public class AgentJumper : MonoModule , IJumpable
    {
        [field:SerializeField] public Rigidbody2D RbCompo {get; private set;}
        [field:Header("Jump Settings")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public bool ResetVelocity = true;
        [field:SerializeField] public UnityEvent OnJumpEvent {get; private set;}
        [SerializeField] private float defaultJumpForce = 5f;

        public void Jump()
            => JumpWithForce(defaultJumpForce);

        public void JumpWithForce(float force)
        {
            if (ResetVelocity)
                RbCompo.linearVelocityY = 0f;
            RbCompo.AddForceY(force, ForceMode2D.Impulse);
        }
    }

    public interface IJumpable
    {
        void Jump();
        void JumpWithForce(float force);
    }
}