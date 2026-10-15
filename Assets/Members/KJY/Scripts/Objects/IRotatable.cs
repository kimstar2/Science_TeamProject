using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface IRotatable
    {
        public void SetDirectionOnTransform(Vector2 direction);
        public void SetDirection(Vector2 direction);
        public void SetAngle(float angle);
    }
}