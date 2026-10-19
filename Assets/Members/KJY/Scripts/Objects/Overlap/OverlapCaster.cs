using System;
using _TevLib.CoreLib;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Objects.Overlap
{
    public abstract class OverlapCaster : MonoBehaviour
    {
        [SerializeField] protected Transform targetTransform;
        [SerializeField] protected bool castWithUpdate = true; 

        [Header("Cast Setting")]
        [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] protected ContactFilter2D contactFilter;
        [SerializeField] protected int count;
        [SerializeField] protected Collider2D[] results;
        public int Count { get; protected set; }

        protected virtual void Awake() => Init();

        protected virtual void Init() => results = new Collider2D[count];

        public abstract void Cast();

        protected void Update()
        {
            if (!castWithUpdate) return;
            Cast();
        }
    }
}