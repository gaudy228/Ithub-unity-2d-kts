using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BulletFactory
{
    private readonly Bullet _prefab;
    private readonly IObjectResolver _resolver;

    public BulletFactory(Bullet prefab, IObjectResolver resolver)
    {
        _prefab = prefab;
        _resolver = resolver;
    }

    public Bullet Create(Vector3 position, LayerMask targetLayerMask, Vector3 dir)
    {
        var bullet = _resolver.Instantiate(_prefab, position, Quaternion.identity);

        bullet.TargetLayerMask = targetLayerMask;
        bullet.Dir = dir;

        return bullet;
    }
}
