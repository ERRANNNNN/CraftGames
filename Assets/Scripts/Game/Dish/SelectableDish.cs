using UnityEngine.EventSystems;
using System;

public class SelectableDish : Dish, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelectDish?.Invoke(data);
    }

    public static Action<DishData> OnSelectDish;

    private void Start()
    {
        Init(data);
    }
}
