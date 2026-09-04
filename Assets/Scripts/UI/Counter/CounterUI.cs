using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private PanelRenderer panelRenderer;
    [Inject] private PlayerData playerData;

    private Label scoreLabel;

    private void OnEnable()
    {
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        scoreLabel = root.Q<Label>("CounterValue");

        var binding = new DataBinding
        {
            dataSource = playerData,
            dataSourcePath = new PropertyPath(nameof(PlayerData.Score)),
            bindingMode = BindingMode.ToTarget
        };
        scoreLabel.SetBinding("text", binding);
    }
}
