using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [Inject] private PlayerData playerData;

    private Label scoreLabel;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        BindUI(root);
    }

    private void OnDisable()
    {
        scoreLabel?.ClearBinding("text");
        scoreLabel = null;
    }

    private void BindUI(VisualElement root)
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