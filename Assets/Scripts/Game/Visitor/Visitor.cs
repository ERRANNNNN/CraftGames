using System.Collections;
using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Visitor : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float duration = 1f;
    [SerializeField] private List<DishData> availableDishes = new List<DishData>();
    [SerializeField] private Transform dishTransform;
    [SerializeField] private Transform visitorDishesPanelTransform;

    private Dictionary<GameObject, DishData> visitorDishes = new Dictionary<GameObject, DishData>();
    public Dictionary<GameObject, DishData> VisitorDishes { get { return visitorDishes; } }

    private Vector3 exitPosition = new Vector2(2078.5f, 540.0f);

    public static Action<Visitor> OnExit;
    public static Action<Visitor> OnVisitorServed;

    public void Init(int dishesCount)
    {
        for (int i = 0; i < dishesCount; i++)
        {
            int availableDishesRandomIndex = RandomNumberGenerator.Generate(availableDishes.Count);
            DishData randomDishData = availableDishes[availableDishesRandomIndex];
            Dish dish = Instantiate(dishTransform, visitorDishesPanelTransform).GetComponent<Dish>();
            visitorDishes.Add(dish.gameObject, randomDishData);
            dish.Init(randomDishData);
        }
    }

    public IEnumerator MoveToPosition(Vector2 destination, bool isExit = false)
    {
        Vector2 startPosition = rect.position;
        float elapsedTime = 0;
        while ((duration - elapsedTime) > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;
            rect.position = Vector2.Lerp(startPosition, destination, percentage);
            yield return new WaitForEndOfFrame();

        }
        if (isExit)
        {
            Destroy(gameObject);
        }
    }

    public void AcceptDish(DishData data)
    {
        GameObject dishObject = visitorDishes.Where(x => x.Value == data).First().Key;
        visitorDishes.Remove(dishObject);
        Destroy(dishObject);
        if (visitorDishes.Count == 0)
        {
            OnExit?.Invoke(this);
            OnVisitorServed?.Invoke(this);
            StartCoroutine(MoveToPosition(exitPosition, true));
        }
    }
}
