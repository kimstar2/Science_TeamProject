using UnityEngine;

namespace Members.KJY.Scripts.Agents
{
    public interface IMoveable
    {
        public Rigidbody2D RbCompo { get; }
        void SetXMoveInput(float xMoveInput);
        void StopImmediately();
    }
}