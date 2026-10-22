using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Objects
{
    public class MagnetFieldP : MonoBehaviour
    {
            [SerializeField] private float _force = 20f;

            private bool _isUp = true;
            public UnityEvent onUp;
            public UnityEvent onDown;

            private void Update()
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    ChangePole();
                }
            }

            public void ChangePole()
            {
                _isUp = !_isUp;
                if (_isUp)
                    onUp?.Invoke();
                else
                    onDown?.Invoke();
            }

            private void OnTriggerEnter2D(Collider2D other)
            {
                if (!other.TryGetComponent<IMagnetableBase>(out _))
                    return;

                Rigidbody2D rb = other.attachedRigidbody;

                if (rb == null)
                    return;

                rb.linearVelocityY = 0f;
            }

            private void OnTriggerStay2D(Collider2D other)
            {
                if (!other.TryGetComponent<IMagnetableBase>(out _))
                    return;

                Rigidbody2D rb = other.attachedRigidbody;

                if (rb == null)
                    return;

                rb.WakeUp();

                Vector2 direction = _isUp ? Vector2.up : Vector2.down;

                rb.AddForce(direction * _force);
        }
    }
}