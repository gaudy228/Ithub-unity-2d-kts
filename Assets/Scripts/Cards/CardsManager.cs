using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private GameObject _cards;

    public void OpenCards()
    {
        _cards.SetActive(true);
    }
}
