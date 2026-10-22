using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using Members.KJY.Scripts.Objects;
using Members.KJY.Scripts.System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Members.KJY.Scripts.Agents.Player
{
    public class Player : ModuleOwner , IMagnetableBase
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private LayerMask noMagnetLayer;
        [SerializeField] private Image clearPanel;
        
        private IInteractable _lastInteractable;
        private IRotatable[] rotateObjects;
        private bool _canUseMagnet = true;

        #region Components 
        
        public Camera MainCam { get; private set; }
        
        public IMoveable AgentMover {get; private set;}
        public AgentAnimator AgentAnimator {get; private set;} // 나중에 인터페이스로 대체
        public IJumpable AgentJumper {get; private set;}
        public IManyJumpable AgentManyJumper {get; private set;}
        public IGroundChecker GroundChecker {get; private set;}
        public ObjectTriggerer Triggerer {get; private set;}
        public HandMagnet HandMagnet {get; private set;}
        
        #endregion
        
        protected override void InitializeModules()
        {
            base.InitializeModules();
            
            MainCam = Camera.main;
            AgentMover = GetModule<IMoveable>(this);
            AgentAnimator = GetModule<AgentAnimator>(this);
            AgentJumper = GetModule<IJumpable>(this);
            AgentManyJumper = GetModule<IManyJumpable>(this);
            GroundChecker = GetModule<IGroundChecker>(this);
            HandMagnet = GetModule<HandMagnet>(this);
            Triggerer = GetModule<ObjectTriggerer>(this);

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
            PlayerInput.OnInteractInput += HandleInteractInput;
            PlayerInput.OnClicked += HandleClicked;

            Triggerer.OnTriggerEnter += HandleTriggerEnter;
            Triggerer.OnTriggerExit += HandleTriggerExit;

            GroundChecker.IsGrounded.OnValueChanged += HandleIsGroundChanged;
        }
        private void OnDisable()
        {
            PlayerInput.SetDisable();
            PlayerInput.OnMoveInput -= HandleMoveInput;
            PlayerInput.OnJumpInput -= HandleJumpInput;
            PlayerInput.OnInteractInput -= HandleInteractInput;
            PlayerInput.OnClicked -= HandleClicked;
            
            Triggerer.OnTriggerEnter -= HandleTriggerEnter;
            Triggerer.OnTriggerExit -= HandleTriggerExit;
            
            GroundChecker.IsGrounded.OnValueChanged -= HandleIsGroundChanged;
        }


        #region Handles
        
        private void HandleMoveInput(float xInput) 
            => AgentMover.SetXMoveInput(xInput);
        
        private void HandleJumpInput() 
            => AgentJumper.Jump();

        private void HandleInteractInput()
        {
            if (_lastInteractable != null)
            {
                _lastInteractable.Interact();
                Debug.Log("Interacted");
            }

        }
        
        private void HandleIsGroundChanged(bool prev, bool next)
        {
            if (!prev && next)
                AgentManyJumper.ResetCrtJump();
        }

        private void RotateToPointer(Vector2 worldPos) 
        {
            foreach (IRotatable rotatable in rotateObjects)
                rotatable.SetDirectionOnTransform(worldPos);
        }
        
        private void HandleClicked(bool isClicked,MouseButton button)
        {
            if (!_canUseMagnet) return;
            var magnetState = button switch
            {
                MouseButton.Left => MagnetState.Push,
                MouseButton.Right => MagnetState.Pull,
                _ => MagnetState.None
            };
            
            HandMagnet.SetState(magnetState,isClicked);
        }

        private void HandleTriggerEnter(ITriggerable trig)
        {
            if (trig.Collider.gameObject.TryGetComponent(out IInteractable interactable))
                _lastInteractable = interactable;
        }
        
        private void HandleTriggerExit(ITriggerable trig)
        {
            if (_lastInteractable?.Collider != trig?.Collider) return;
            _lastInteractable = null;
        }
        
        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Clear"))
                clearPanel.gameObject.SetActive(true);
            
            if (!other.CompareTag("NoMagnet")) return;
            HandMagnet.SetState(HandMagnet.CurrentState,false);
            _canUseMagnet = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("NoMagnet")) return;
            _canUseMagnet = true;
        }
    }
}
