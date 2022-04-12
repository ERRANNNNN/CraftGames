using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> slots = new List<Transform>();
    [SerializeField] private Transform visitorRect;
    [SerializeField] private Transform canvas;
    [SerializeField] private float movingToPositionDelay = 1;
    [SerializeField] private int maxVisitorsNumber = 6;
    [SerializeField] private int maxDishesNumber = 14;
    [SerializeField] private int maxDishesPerVisitor = 3;
    [SerializeField] private int minDishesPerVisitor = 1;

    public static System.Random rand = new System.Random();
    private List<Visitor> visitors = new List<Visitor>();
    private int servicedVisitors = 0;
    private int spawnedVisitors = 0;
    private int currentDishesCount = 0;
    private Vector2 startVisitorPosition = new Vector2(-207.8f, 594.0f);

    private void Start()
    {
        Visitor.onExit += DeleteVisitorFromQueue;
        SpawnVisitors();
    }

    private void SpawnVisitors()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            visitors.Add(SpawnVisitor());
        }
        StartCoroutine(MoveVisitorsToPositions());
    }

    IEnumerator MoveVisitorsToPositions()
    {
        foreach(Visitor visitor in visitors.ToArray())
        {
            int indexInSlots = (slots.Count - 1) - visitors.IndexOf(visitor);
            if (visitor.transform.position != slots[indexInSlots].position)
            {
                StartCoroutine(visitor.MoveToPosition(slots[indexInSlots].position));
                yield return new WaitForSeconds(movingToPositionDelay);
            }
        }
        movingToPositionDelay = 0;
    }

    private void DeleteVisitorFromQueue(Visitor visitor)
    {
        StopCoroutine(nameof(MoveVisitorsToPositions));
        if (maxVisitorsNumber == spawnedVisitors)
        {
            visitors.Remove(visitor);
        } else
        {
            visitors.Remove(visitor);
            visitors.Add(SpawnVisitor());
        }
        servicedVisitors++;
        StartCoroutine(MoveVisitorsToPositions());
    }

    private void OnDestroy()
    {
        Visitor.onExit -= DeleteVisitorFromQueue;
    }

    private Visitor SpawnVisitor()
    {
        Visitor visitor = Instantiate(visitorRect, canvas).GetComponent<Visitor>();
        visitor.transform.SetAsFirstSibling();
        int randCount = rand.Next(minDishesPerVisitor, maxDishesPerVisitor + 1);
        float remainingDishesCount = (maxDishesNumber - (currentDishesCount + randCount));
        float remainingVisitorsCount = (maxVisitorsNumber - (spawnedVisitors + 1));
        while (
            (remainingDishesCount > (remainingVisitorsCount * maxDishesPerVisitor))
            || ((remainingDishesCount / remainingVisitorsCount) < 1)
        )
        {
            remainingDishesCount = (maxDishesNumber - (currentDishesCount + randCount));
            remainingVisitorsCount = (maxVisitorsNumber - (spawnedVisitors + 1));
            if (remainingDishesCount > (remainingVisitorsCount * maxDishesPerVisitor))
            {
                randCount++;
            }
            else if ((remainingDishesCount / remainingVisitorsCount) < 1)
            {
                randCount--;
            }
        }
        currentDishesCount += randCount;
        spawnedVisitors++;
        visitor.Init(randCount);
        return visitor;
    }
}
