using _TevLib.ModuleSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Agents
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class AgentAnimator : MonoModule
    {
        private Animator _animator;
        private readonly int _xDirHash = Animator.StringToHash("xDir");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetFloat(int hash, float value) 
            => _animator.SetFloat(hash,value);
    }
}