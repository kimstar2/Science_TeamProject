using Unity.Cinemachine;
using UnityEngine;

namespace Members.KJY.Scripts.System
{
    public class CinemachinePrioritySetter : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCam;
        
        private const int MainPriority = 100;
        private const int DefaultPriority = 0;

        public void SetPriority(int priority)
        {
            cinemachineCam.Priority = priority;
        }
        
        public void EnterCam() 
            => SetPriority(MainPriority);
        
        public void ExitCam()
            => SetPriority(DefaultPriority);
    }
}