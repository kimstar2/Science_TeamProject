using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Proto.Scripts.System
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
        
        #region Callbacks
        public void OnMove(InputAction.CallbackContext context) 
            => OnMoveInput?.Invoke(context.ReadValue<Vector2>().x);

        public void OnLook(InputAction.CallbackContext context)
        {
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            
        }
        
        #endregion
    }
}