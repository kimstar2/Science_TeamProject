using System;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface ITriggerable
    {
        Collider2D Collider { get; }
        void EnterTrigger();
        void ExitTrigger();
    }
}