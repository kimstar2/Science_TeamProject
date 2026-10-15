using System;
using DG.Tweening;
using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public interface IMagnetable
    {
        MagnetablePropertySO MagnetableProp { get; }
        public void Pull(HandMagnet magnet, float force);
        void Push(Vector3 magmetPos,Vector2 direction, float force);
    }
}