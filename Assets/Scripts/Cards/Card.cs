using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [Inject] private PlayerSpecialAttack _playerSpecialAttack;

    [SerializeField] private GameObject _cards;

    [SerializeField] private SpecialDecoratorType _decoratorType;

    [SerializeField] private TextMeshProUGUI _textName;

    private void Start()
    {
        _textName.text = _decoratorType.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _playerSpecialAttack.AddDecorator(_decoratorType);
        _cards.SetActive(false);
    }
}
