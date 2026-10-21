using System;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface ITriggerable
    {
        public bool IsEntered {get; }
        Collider2D Collider { get; }
        void EnterTrigger();
        void ExitTrigger();
    }
}