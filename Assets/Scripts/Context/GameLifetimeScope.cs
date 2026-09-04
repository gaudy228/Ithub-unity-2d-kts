using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Rigidbody2D _rbPlayer;
    [SerializeField] private float _jumpForce;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerInput>(Lifetime.Singleton);

        builder.Register<PlayerJumpLogic>(Lifetime.Singleton)
            .WithParameter(typeof(Rigidbody2D), _rbPlayer)
            .WithParameter(typeof(float), _jumpForce);

        builder.Register<PlayerData>(Lifetime.Singleton);

        builder.Register<GameObjectFactory>(Lifetime.Singleton);
    }
}
