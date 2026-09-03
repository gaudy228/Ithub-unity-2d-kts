using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb;


    private PlayerInput _input;
    private PlayerJumpLogic _jumpLogic;

    private void Start()
    {
        _input = new PlayerInput();
        _jumpLogic = new PlayerJumpLogic(_jumpForce, _rb);

        Subscriptions();
    }

    private void Subscriptions()
    {
        _input.OnUpPressed += _jumpLogic.Jump;
    }

    private void OnDisable()
    {
        _input.OnDisable();
    }
}
