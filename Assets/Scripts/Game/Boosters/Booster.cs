using UnityEngine;
using System;
using UnityEngine.EventSystems;
using TMPro;

public class Booster : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI boosterCountTextMesh;
    public static Action OnBoosterUsed;
    private int boosterCount;

    public void Init(int _boosterCount)
    {
        boosterCount = _boosterCount;
        boosterCountTextMesh.text = boosterCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boosterCount != 0)
        {
            OnBoosterUsed?.Invoke();
            boosterCount--;
            boosterCountTextMesh.text = boosterCount.ToString();
        }
    }
}
