using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Visitor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float duration = 1f;
    [SerializeField] private List<DishData> availableDishes = new List<DishData>();
    [SerializeField] private Transform dishTransform;
    [SerializeField] private Transform visitorDishesPanelTransform;
    private List<DishData> visitorDishes = new List<DishData>();
    

    private Vector3 exitPosition = new Vector2(2078.5f, 540.0f);

    public static Action<Visitor> onExit;

    public void Init(int dishesCount)
    {
        for (int i = 0; i < dishesCount; i++)
        {
            int availableDishesRandomIndex = VisitorsSpawner.rand.Next(availableDishes.Count);
            DishData randomDishData = availableDishes[availableDishesRandomIndex];
            visitorDishes.Add(randomDishData);
            Dish dish = Instantiate(dishTransform, visitorDishesPanelTransform).GetComponent<Dish>();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        onExit?.Invoke(this);
        StartCoroutine(MoveToPosition(exitPosition, true));
    }
}
