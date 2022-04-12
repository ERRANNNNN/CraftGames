using UnityEngine.EventSystems;
using System;

public class SelectableDish : Dish, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelectDish?.Invoke(data);
    }

    public static Action<DishData> OnSelectDish;

    // Start is called before the first frame update
    void Start()
    {
        Init(data);
    }
}
