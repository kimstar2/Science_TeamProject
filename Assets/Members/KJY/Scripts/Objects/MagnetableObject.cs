using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Members.KJY.Scripts.Utils;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public class MagnetableObject : MonoBehaviour , IMagnetable
    {
        [SerializeField] private Transform originParent;
        [SerializeField] private float pullForce = 0.05f;
        [SerializeField] private float pullOffRange;
        
        private Rigidbody2D _rigidCompo;
        private float _originGravity;

        private CancellationTokenSource _moveCts;
        private Transform _targetTrm;
        private float _currentForce;
        
        public bool IsPulling {get; private set;}

        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
            _originGravity = _rigidCompo.gravityScale;
        }

        public void Pull(Transform targetTrm, float force , bool isClicked)
        {
            _currentForce = force;
            IsPulling = isClicked;
            
            _targetTrm = IsPulling ? targetTrm : null;
        }

        private void PullLogic()
        {
            if (!IsPulling) return;
            Vector2 dir = _targetTrm.position - transform.position;
            
            if (dir.sqrMagnitude > pullOffRange * pullOffRange)
            {
                IsPulling = false;
                return;
            }
            
            Vector2 crtVelocity = _rigidCompo.linearVelocity;
            _rigidCompo.linearVelocity = Vector2.MoveTowards(crtVelocity,dir.normalized * _currentForce/_rigidCompo.mass, pullForce * Time.fixedDeltaTime);
        }
        
        private void FixedUpdate()
        {
            PullLogic();
        }

        public void Push(Vector2 direction, float force)
        {
            _rigidCompo.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}