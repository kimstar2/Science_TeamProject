using System;
using UnityEngine;

namespace Members.KJY.Proto.Scripts.Agents
{
    public class AgentMover : MonoBehaviour
    {
        [field:SerializeField] public Rigidbody2D RigidCompo {get; private set;}
        
        [Header("Speed Settings")]
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float maxSpeed;
        private float _xMoveInput;

        private float m_speed;
        public float Speed
        {
            get => m_speed;
            set
            {
                if (value < 0f)
                    return;
                m_speed = Mathf.Min(maxSpeed);
            }
        }
            
        private void Awake() 
            => Speed = defaultSpeed;

        private void FixedUpdate()
        {
            RigidCompo.linearVelocityX = _xMoveInput * Speed;
        }
        
        public void SetXMoveInput(float xMoveInput) 
            => _xMoveInput = xMoveInput;

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
    }
}