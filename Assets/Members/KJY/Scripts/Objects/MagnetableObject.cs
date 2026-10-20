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
        private IMagnet _pullMagnet;
        private float _beforeSqrDistance;
        
        
        public Rigidbody2D RigidCompo { get; private set; }
        private float _crtGravity;
        private float _defaultGravity;
        
        private float _currentForce;

        private void Awake()
        {
            RigidCompo = GetComponent<Rigidbody2D>();
            _defaultGravity = RigidCompo.gravityScale;
        }

        private void FixedUpdate()
        {
            PullLogic();
        }
        
        public void Pull(IMagnet magnet, float force)
        {
            _currentForce = force;
            _pullMagnet = magnet;
        }

        private void PullLogic()
        {
            if (_pullMagnet is { IsClicked: true } and { CurrentState: MagnetState.Pull })
            {
                Vector2 dir = _pullMagnet.MagnetPivot.position - transform.position;
                float distanceDiff = dir.sqrMagnitude - _beforeSqrDistance;
                distanceDiff = Mathf.Abs(distanceDiff);


                if (dir.sqrMagnitude > MProp.PullOffRange * MProp.PullOffRange || distanceDiff > MProp.PullCorrectRange * MProp.PullCorrectRange)
                {
                    OutPull();
                    return;
                }

                SetGravity();
                Vector2 crtVelocity = RigidCompo.linearVelocity;
                Vector2 targetVel = dir.normalized * (_currentForce * (dir.sqrMagnitude + MProp.PullInstability)) /
                                    MProp.PullStability;
                RigidCompo.linearVelocity =
                    Vector2.MoveTowards(crtVelocity, targetVel, MProp.PullForce * Time.fixedDeltaTime);
                _beforeSqrDistance = dir.sqrMagnitude;
            }
            else
                OutPull();
        }

        public void OutPull()
        {
            _pullMagnet = null;
            SetGravity(_defaultGravity);
        }
        
        private void SetGravity(float g = 0f)
        {
            if (Mathf.Approximately(g, _crtGravity)) return;
            _crtGravity = g;
            RigidCompo.gravityScale = _crtGravity;
        }

        public void Push(Vector3 magnetPos,Vector2 direction, float force)
        {
            float magnitude = magnetPos.magnitude;
            RigidCompo.AddForce(direction * Mathf.Min(force / magnitude,MProp.MaxPushForce), ForceMode2D.Impulse);
        }
    }
}