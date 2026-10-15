using DG.Tweening;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface IMagnetable
    {
        bool IsPulling { get;}
        public void Pull(Transform targetTrm, float force, bool isClicked);
        void Push(Vector2 direction, float force);
    }
}