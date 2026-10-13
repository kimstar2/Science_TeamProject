using UnityEngine;
using UnityEngine.InputSystem;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float _power = 10f;

    private bool _isAttract = true;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
            _isAttract = !_isAttract;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;
        Vector2 direction = (transform.position - collision.transform.position).normalized;

        rb.AddForce(direction * (_isAttract ? _power : -_power));
    }
}