using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using Members.KJY.Scripts.Objects;
using Members.KJY.Scripts.System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

namespace Members.KJY.Scripts.Agents.Player
{
    public class ProtoPlayer : ModuleOwner
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: ShowNonSerializedField] public MagnetState CurrentMagnetType { get; private set; }
        
        private IRotatable[] rotateObjects;
        
        #region Components 
        
        public Camera MainCam { get; private set; }
        
        public AgentAnimator AgentAnimator {get; private set;} // 나중에 인터페이스로 대체
        public IMoveable AgentMover {get; private set;}
        public IJumpable AgentJumper {get; private set;}
        public HandMagnet HandMagnet {get; private set;}
        
        
        #endregion
        
        protected override void InitializeModules()
        {
            base.InitializeModules();
            
            MainCam = Camera.main;
            AgentMover = GetModule<IMoveable>(this);
            AgentJumper = GetModule<IJumpable>(this);
            AgentAnimator = GetModule<AgentAnimator>(this);
            HandMagnet = GetModule<HandMagnet>(this);

            rotateObjects = GetComponentsInChildren<IRotatable>();
        }

        private void Update()
        {
            Vector2 worldPos = MainCam.ScreenToWorldPoint(PlayerInput.PointerInput);
            RotateToPointer(worldPos);
            HandMagnet.SetDirection(worldPos);
        }

        private void OnEnable()
        {
            PlayerInput.SetEnable();
            PlayerInput.OnMoveInput += HandleMoveInput;
            PlayerInput.OnJumpInput += HandleJumpInput;
            PlayerInput.OnClicked += HandleClicked;
        }

        private void OnDisable()
        {
            PlayerInput.SetDisable();
            PlayerInput.OnMoveInput -= HandleMoveInput;
            PlayerInput.OnJumpInput -= HandleJumpInput;
            PlayerInput.OnClicked -= HandleClicked;
        }
        
        #region Handles
        
        private void HandleMoveInput(float xInput) 
            => AgentMover.SetXMoveInput(xInput);
        
        private void HandleJumpInput() 
            => AgentJumper.Jump();
        
        private void RotateToPointer(Vector2 worldPos) 
        {
            foreach (IRotatable rotatable in rotateObjects)
                rotatable.SetDirectionOnTransform(worldPos);
        }
        
        private void HandleClicked(bool isClicked,MouseButton button)
        {
            CurrentMagnetType = button switch
            {
                MouseButton.Left => MagnetState.Push,
                MouseButton.Right => MagnetState.Pull,
                _ => MagnetState.None
            };
            
            HandMagnet.SetState(CurrentMagnetType,isClicked);
        }

        #endregion
        
        
        
    }
}
