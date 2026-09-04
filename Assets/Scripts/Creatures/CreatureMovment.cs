using UnityEngine;

public class CreatureMovment : MonoBehaviour
{
    private const float _deathTime = 15f;
    [SerializeField] private float _speed;

    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        Destroy(gameObject, _deathTime);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(-_speed, _rb.linearVelocity.y);
    }
}
