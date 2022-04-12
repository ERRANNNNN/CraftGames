using UnityEngine;
using UnityEngine.UI;

public class Dish : MonoBehaviour
{
    [SerializeField] protected DishData data;
    [SerializeField] protected Image image;

    public void Init(DishData _data)
    {
        data = _data;
        image.sprite = data.sprite;
    }
}
