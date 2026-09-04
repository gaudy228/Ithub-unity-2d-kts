using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameObjectFactory
{
    private readonly IObjectResolver _resolver;

    public GameObjectFactory(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    public GameObject Create(GameObject prefab)
    {
        return _resolver.Instantiate(prefab);
    }

    public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return _resolver.Instantiate(prefab, position, rotation);
    }
}
