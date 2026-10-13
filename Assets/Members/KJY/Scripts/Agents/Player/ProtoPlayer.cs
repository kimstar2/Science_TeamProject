using _TevLib.ModuleSystem;
using Members.KJY.Scripts.System;
using UnityEngine;

namespace Members.KJY.Scripts.Agents.Player
{
    public class ProtoPlayer : ModuleOwner
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        #region Components 
        
        public AgentAnimator AgentAnimator {get; private set;}
        public AgentMover AgentMover {get; private set;}
        
        #endregion
        
        protected override void InitializeModules()
        {
            base.InitializeModules();
            
            AgentMover = GetModule<AgentMover>();
            AgentAnimator = GetModule<AgentAnimator>();
        }

        private void OnEnable()
        {
            PlayerInput.SetEnable();
            PlayerInput.OnMoveInput += HandleMoveInput;
        }

        private void OnDisable()
        {
            PlayerInput.SetDisable();
            PlayerInput.OnMoveInput -= HandleMoveInput;
        }
        
        #region Handles
        
        private void HandleMoveInput(float xInput)
        {
            AgentMover.SetXMoveInput(xInput);
        }

        #endregion
        
        
    }
}
