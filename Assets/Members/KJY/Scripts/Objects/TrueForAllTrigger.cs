using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using ZLinq;

namespace Members.KJY.Scripts.Objects
{
    public class TrueForAllTrigger : MonoBehaviour
    {
        [SerializeField] private AbstractTrigger[] buttons;
        [SerializeField] private bool onlyOnce;
        public UnityEvent allTrue;
        public UnityEvent fail;
        private bool _ok;
        private bool _isTrue;
        
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
            if (_isTrue) return;
            bool ok = buttons.AsValueEnumerable().ToList().TrueForAll(s=>s.IsEntered);;
            
            if (_ok == ok) return;
            _ok = ok;

            
            if (_ok)
            {
                if (onlyOnce)
                    _isTrue = true;
                allTrue?.Invoke();
            }
            else
                fail?.Invoke();
        }
    }
}