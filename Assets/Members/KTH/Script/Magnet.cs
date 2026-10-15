using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float _power = 10f;
    [SerializeField] private MagnetField _magneticField;

    private bool _isAttract = true;

    public void ChangeMagnet()
    {
        _isAttract = !_isAttract;

        _magneticField.ChangePole();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;

        Vector2 direction = (transform.position - collision.transform.position).normalized;

        rb.AddForce(direction * (_isAttract ? _power : -_power));
    }
}