using System;

public class PlayerInput
{
    public Action OnJumpPressed;

    private Control _control;

    public PlayerInput()
    {
        _control = new Control();
        _control.Main.Enable();
        _control.Main.Jump.performed += ctx => OnJumpPressed?.Invoke();
    }

    public void OnDisable()
    {
        _control.Disable();
    }
}