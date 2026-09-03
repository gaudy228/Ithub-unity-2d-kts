using UnityEngine;

public class PlayerJumpLogic
{
    private float _jumpForce;
    private Rigidbody2D _rb;

    public PlayerJumpLogic(float jumpForce, Rigidbody2D rb)
    {
        _jumpForce = jumpForce;
        _rb = rb;
    }

    public void Jump()
    {
        _rb.velocity = Vector3.zero;
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
