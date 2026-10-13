using System;
using UnityEngine;

namespace Members.KJY.Proto.Scripts.Agents
{
    public class AgentAnimator : MonoBehaviour
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