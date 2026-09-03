using System;

public class PlayerInput
{
    public Action OnUpPressed;

    private Control _control;

    public PlayerInput()
    {
        _control = new Control();
        _control.Main.Enable();
        _control.Main.Jump.performed += ctx => OnUpPressed?.Invoke();
    }

    public void OnDisable()
    {
        _control.Disable();
    }
}
