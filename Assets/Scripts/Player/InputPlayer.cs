using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private PlayerInputs _inputs;
    private PlayerMove _move;
    private PlayerAttack _attack;
    private PlayerSpecialAttack _specialAttack;
    [SerializeField] private CardsManager _cardsManager;

    private void Awake()
    {
        _inputs = new PlayerInputs();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<PlayerAttack>();
        _specialAttack = GetComponent<PlayerSpecialAttack>();

        _inputs.Enable();
    }

    private void OnEnable()
    {
        _inputs.Player.Attack.performed += OnAttack;
        _inputs.Player.SpecialAttack.performed += OnSpecialAttack;
        _inputs.Player.OpenCards.performed += OnOpenCards;
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Update()
    {
        ReadMovement();
    }

    private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _attack.Attack();
    }

    private void OnSpecialAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _specialAttack.Attack();
    }

    private void OnOpenCards(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _cardsManager.OpenCards();
    }

    private void ReadMovement()
    {
        Vector2 inputDirection = _inputs.Player.Move.ReadValue<Vector2>().normalized;

        _move.Move(inputDirection);
    }
}
