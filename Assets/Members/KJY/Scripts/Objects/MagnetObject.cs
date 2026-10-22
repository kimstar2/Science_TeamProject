using Members.KJY.Scripts.Agents.Player.Equipment.Magnetic;
using Members.KJY.Scripts.Objects.Overlap;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Objects
{
    public class MagnetObject : BoxCaster , IMagnet
    {
        [field:SerializeField] public MagnetState CurrentState { get; private set; }
        [field:Header("Magnet Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public Transform MagnetPivot { get; private set; }
        [SerializeField] private float defaultPullForce;
        [SerializeField] private float defaultPushForce;
        public UnityEvent onPull;
        public UnityEvent onPush;

        public bool IsClicked { get; private set; } = true;

        protected override void Init()
        {
            base.Init();
            SetState(MagnetState.Push,true);
        }

        public void SetState(MagnetState state, bool isClicked)
        {
            if (CurrentState != state && GetClicked(CurrentState)) return;

            IsClicked = isClicked;
            CurrentState = !isClicked ? MagnetState.None : state;
        }

        public bool GetClicked(MagnetState state) => CurrentState == state && IsClicked;
        
        public void SetDirection(Vector2 worldPos) { }
        
        public override void Cast()
        {
            base.Cast();
            for (int i = 0; i < Count; i++)
            {
                if (results[i].TryGetComponent(out IMagnetable magnetable))
                    switch (CurrentState)
                    {
                        case MagnetState.Pull:
                            onPull?.Invoke();
                            magnetable.Pull(this, defaultPullForce);
                            break;
                        case MagnetState.Push:
                            Vector2 direction = results[i].transform.position - transform.position;
                            onPush?.Invoke();
                            magnetable.Push(transform.position,direction.normalized, defaultPushForce);
                            break;
                    }
            }   
        }
    }
}