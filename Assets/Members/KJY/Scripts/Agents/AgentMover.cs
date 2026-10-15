using _TevLib.ModuleSystem;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Agents
{
    public class AgentMover : MonoModule , IMoveable
    {
        [field:SerializeField] public Rigidbody2D RbCompo {get; private set;}
        
        [Header("Speed Setting")]
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float maxSpeed;
        private float _xMoveInput;
        private float _yMoveInput;
        
        private float m_speed;
        public float Speed
        {
            get => m_speed;
            private set => m_speed = Mathf.Clamp(value,0f,maxSpeed);
        }
            
        private void Awake() 
            => Speed = defaultSpeed;

        private void FixedUpdate()
        {
            RbCompo.linearVelocityX = _xMoveInput * Speed;
        }

        public void SetXMoveInput(float xMoveInput) 
            => _xMoveInput = xMoveInput;

        public void StopImmediately()
        {
            SetXMoveInput(0f);
            RbCompo.linearVelocityX = 0f;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
    }
}