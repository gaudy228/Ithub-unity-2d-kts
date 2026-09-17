using UnityEngine;

public class ConfigTest : MonoBehaviour
{
    private async void Start()
    {
        var loader = new RemoteConfigLoader();
        var weapons = await loader.LoadAsync();
        Debug.Log($"Итого оружий: {weapons.Count}");
    }
}
