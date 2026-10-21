using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using ZLinq;

namespace Members.KJY.Scripts.Objects
{
    public class TrueForAllTrigger : MonoBehaviour
    {
        [SerializeField] public AbstractTrigger[] buttons;
        public UnityEvent allTrue;
        public UnityEvent fail;
        private bool _ok;

        private void Awake()
        {
            foreach (AbstractTrigger button in buttons)
            {
                button.OnTrigger += HandleTrigger;
            }
        }

        private void OnDestroy()
        {
            foreach (AbstractTrigger button in buttons)
            {
                button.OnTrigger -= HandleTrigger;
            }
        }

        private void HandleTrigger()
        {
            bool ok = buttons.AsValueEnumerable().ToList().TrueForAll(s=>s.IsEntered);;
            
            if (_ok == ok) return;
            _ok = ok;
            
            if (_ok)
                allTrue?.Invoke();
            else
                fail?.Invoke();
        }
    }
}