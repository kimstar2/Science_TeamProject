using System;
using DG.Tweening;
using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface IMagnetable
    {
        MagnetablePropertySO MagnetableProp { get; }
        public void Pull(IMagnet magnet, float force);
        void Push(Vector3 magnetPos,Vector2 direction, float force);
        void OutPull();
    }
}