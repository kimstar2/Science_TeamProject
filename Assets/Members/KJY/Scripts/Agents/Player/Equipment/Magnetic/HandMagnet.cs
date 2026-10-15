using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Objects;
using Members.KJY.Scripts.Utils;
using NaughtyAttributes;
using UnityEngine;
using ZLinq;

namespace Members.KJY.Scripts.Agents.Player.Equipment.Magnetic
{
    public class HandMagnet : MonoModule
    {
        [field:Header("Target Setting")]
        [field:HorizontalLine(color:EColor.Gray)] // <- 이거는 그냥 인스펙터에 선 띄워주는 용도
        [field:SerializeField] public ContactFilter2D ContactFilter { get; private set; }
        [SerializeField] private int defaultCount;
        private Collider2D[] _results; // 구조체길래 사용함
        private int _count;
        
        [Header("Magnet Settings")]
        [field:HorizontalLine(color:EColor.Gray)]
        [SerializeField] private Transform magnetPivot;
        [SerializeField] private int defaultPullForce;
        [SerializeField] private int defaultPushForce;
        [SerializeField] private float radius = 0.5f;
        
        [Header("Render Settings")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public MonoRenderer Renderer;
        [SerializeField] private Color pullColor = Color.red;
        [SerializeField] private Color pushColor = Color.blue;
        
        private readonly List<IMagnetable> _magnetable = new();
        
        private Vector2 _direction;
        private float _pullForce;
        private float _pushForce;
        
        public override void Init(ModuleOwner owner)
        {
            base.Init(owner);
            _pullForce = defaultPullForce;
            _pushForce = defaultPushForce;
            _count = defaultCount;
            _results = new Collider2D[defaultCount];
        }
        
        public void SetDirection(Vector2 worldPos)
        {
            Vector2 dir = (Vector3)worldPos - transform.position;
            _direction = dir.normalized;
        }

        public void SetState(MagnetState state, bool isClicked)
        {   
            switch (state)
            {
                case MagnetState.None:
                default:
                    return;
                
                case MagnetState.Pull:
                    Pull(isClicked);
                    break;
                case MagnetState.Push:
                    PushLogic(isClicked);
                    break;
            }
        }

        private void Pull(bool isClicked)
        {
            Renderer.SetColor(isClicked ? pullColor : Color.white);

            foreach (var magnetable in GetMagnetable())
                magnetable.Pull(transform, _pullForce, isClicked);
        }

        private void PushLogic(bool isClicked)
        {
            Renderer.SetColor(isClicked ? pushColor : Color.white);
            
            if (!isClicked) return;
            
            foreach (var magnetable in GetMagnetable())
            {
                if (magnetable.IsPulling) continue;
                magnetable.Push(_direction, _pushForce);
            }
        }

        #region Helper

        private IMagnetable[] GetMagnetable()
        {
            _magnetable.Clear();
            for (int i = 0; i < DetectObjects(); i++)
            {
                if (_results[i].TryGetComponent(out IMagnetable magnetable))
                    _magnetable.Add(magnetable);
            }
            return _magnetable.AsValueEnumerable().ToArray();
        }

        private int DetectObjects()
        {
            _count = Physics2D.OverlapCircle(magnetPivot.position, radius, ContactFilter, _results);
            return _count;
        }

        #endregion

    }
}