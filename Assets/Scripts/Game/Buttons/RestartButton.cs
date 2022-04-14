using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnRestart?.Invoke();
    }

    public static Action OnRestart;
}
