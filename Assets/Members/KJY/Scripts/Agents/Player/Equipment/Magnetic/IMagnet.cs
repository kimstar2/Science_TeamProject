using UnityEngine;

namespace Members.KJY.Scripts.Agents.Player.Equipment.Magnetic
{
    public interface IMagnet
    {
        public void SetState(MagnetState state, bool isClicked);
        public void SetDirection(Vector2 worldPos);
        Transform MagnetPivot { get; }
        public bool IsClicked { get; }
        public MagnetState CurrentState { get; }
    }
}