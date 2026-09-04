using UnityEngine;
using VContainer;

public class Player : MonoBehaviour
{
    [Inject] private PlayerInput _input;
    [Inject] private PlayerJumpLogic _jumpLogic;

    private void Start()
    {
        Subscriptions();
    }

    private void Subscriptions()
    {
        _input.OnJumpPressed += _jumpLogic.Jump;
    }

    private void OnDisable()
    {
        _input.OnDisable();
    }
}
