using UnityEngine;

public class InvisibilityAttackState : State
{
    private const float _targetAlpha = 0.2f;
    private const float _defaultAlpha = 1f;

    private PlayerAttack _playerAttack;

    private SpriteRenderer _spriteRenderer;

    public InvisibilityAttackState(PlayerAttack playerAttack, SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
        _playerAttack = playerAttack;
    }

    public override void Enter()
    {
        ChangeAlpha(_targetAlpha);
    }

    public override void Exit()
    {
        ChangeAlpha(_defaultAlpha);
    }

    private void ChangeAlpha(float alpha)
    {
        Color currentColor = _spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

        _spriteRenderer.color = newColor;
    }
}
