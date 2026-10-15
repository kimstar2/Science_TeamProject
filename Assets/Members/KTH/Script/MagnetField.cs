using Members.KJY.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetField : MonoBehaviour
{
    [SerializeField] private float _force = 20f;

    private bool _isUp = true;

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
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent<IMagnetable>(out _))
            return;

        Rigidbody2D rb = other.attachedRigidbody;

        if (rb == null)
            return;

        rb.WakeUp();

        Vector2 direction = _isUp ? Vector2.up : Vector2.down;
        rb.AddForce(direction * _force);
    }
}