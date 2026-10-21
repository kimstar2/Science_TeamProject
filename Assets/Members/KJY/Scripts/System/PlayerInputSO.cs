using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Members.KJY.Scripts.System
{
    [CreateAssetMenu(fileName = "Player Input Data", menuName = "KJY/System/PlayerInput Data", order = 0)]
    public class PlayerInputSO : ScriptableObject , Controls.IPlayerActions
    {
        private Controls _controls;

        #region Settings

        public void SetEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }
        
        public void SetDisable() => _controls?.Player.Disable();

        #endregion
        
        public event Action<float> OnMoveInput;
        public event Action<bool,MouseButton> OnClicked;
        public event Action OnJumpInput;
        public event Action OnInteractInput;
        public Vector2 PointerInput {get; private set;}
        
        
        #region Callbacks
        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>().x);
        }

        public void OnPointer(InputAction.CallbackContext context)
        {
            PointerInput =context.ReadValue<Vector2>();
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnClicked?.Invoke(true, MouseButton.Left);

            if (context.canceled)
                OnClicked?.Invoke(false, MouseButton.Left);
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnClicked?.Invoke(true, MouseButton.Right);

            if (context.canceled)
                OnClicked?.Invoke(false, MouseButton.Right);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumpInput?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractInput?.Invoke();
        }


        #endregion
    }
}