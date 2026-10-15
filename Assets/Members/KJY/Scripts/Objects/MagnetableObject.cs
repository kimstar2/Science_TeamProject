using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using Members.KJY.Scripts.Utils;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    public class MagnetableObject : MonoBehaviour , IMagnetable
    {
        [field:SerializeField] public MagnetablePropertySO MagnetableProp { get; private set; }
        private MagnetablePropertySO MProp => MagnetableProp;
        private HandMagnet _PullMagnet;
        
        
        private Rigidbody2D _rigidCompo;
        private float _crtGravity;
        private float _defaultGravity;
        
        private float _currentForce;

        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
            _defaultGravity = _rigidCompo.gravityScale;
        }

        private void FixedUpdate()
        {
            PullLogic();
        }
        
        public void Pull(HandMagnet magnet, float force)
        {
            _currentForce = force;
            _PullMagnet = magnet;
        }

        private void PullLogic()
        {
            if (_PullMagnet is { IsClicked: true } and { CurrentState: MagnetState.Pull })
            {
                Vector2 dir = _PullMagnet.MagnetPivot.position - transform.position;
                if (dir.sqrMagnitude > MProp.PullOffRange * MProp.PullOffRange)
                {
                    _PullMagnet = null;
                    SetGravity(_defaultGravity);
                    return;
                }
            
                SetGravity();
                Vector2 crtVelocity = _rigidCompo.linearVelocity;
                Vector2 targetVel = dir.normalized * (_currentForce * (dir.sqrMagnitude + MProp.PullInstability));
                _rigidCompo.linearVelocity = Vector2.MoveTowards(crtVelocity,targetVel, MProp.PullForce * Time.fixedDeltaTime);
            }
            else
            {
                _PullMagnet = null;
                SetGravity(_defaultGravity);
            }
        }

        private void SetGravity(float g = 0f)
        {
            if (Mathf.Approximately(g, _crtGravity)) return;
            _crtGravity = g;
            _rigidCompo.gravityScale = _crtGravity;
        }

        public void Push(Vector3 magnetPos,Vector2 direction, float force)
        {
            float magnitude = magnetPos.magnitude;
            _rigidCompo.AddForce(direction * Mathf.Min(force / magnitude,MProp.MaxPushForce), ForceMode2D.Impulse);
        }
    }
}