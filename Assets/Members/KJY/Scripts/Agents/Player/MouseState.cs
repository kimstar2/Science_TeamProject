    using UnityEngine.InputSystem.LowLevel;

namespace Members.KJY.Scripts.Agents.Player
{
    public readonly struct MouseState
    {
        public readonly MouseButton mouseButton;
        public readonly bool isClicked;

        public MouseState(bool isClicked, MouseButton mouseButton)
        {
            this.isClicked = isClicked;
            this.mouseButton = mouseButton;
        }
    }
}