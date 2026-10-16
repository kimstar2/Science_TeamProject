    using System;
    using _TevLib.CoreLib;

    namespace Members.KJY.Scripts.Agents
    {
        public interface IGroundChecker
        {
            bool CheckGround { get; }
            NotifyValue<bool> IsGrounded { get; }
        }
    }