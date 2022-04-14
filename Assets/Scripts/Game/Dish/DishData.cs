using UnityEngine;

[CreateAssetMenu(fileName = "DishData", menuName = "ScriptableObjects/Dish", order = 1)]
public class DishData : ScriptableObject
{
    public Sprite sprite;
    public string dishName;
}
