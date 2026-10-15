using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public class RotateObject : MonoBehaviour , IRotatable
    {
        [field:SerializeField] public Transform BaseTransform { get; private set; }
        [field:SerializeField] public Transform TargetTransform {get; private set;}
        [SerializeField] private float maxRange;

        public void SetAngle(float angle) 
            => TargetTransform.eulerAngles = Vector3.forward * angle;

        public void SetDirection(Vector2 direction)
        {
            if (Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0)) return;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            SetAngle(angle);
        }

        public void SetDirectionOnTransform(Vector2 direction)
        {
            if (Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0)) return;
            Vector2 dir = (Vector3)direction - BaseTransform.position;
            SetDirection(dir);

            if (dir.sqrMagnitude > maxRange * maxRange)
            {
                Vector2 rangedDir = dir.normalized;
                rangedDir *= maxRange;
                TargetTransform.localPosition = rangedDir;
                return;
            }
            TargetTransform.position = direction;
        }
    }
}