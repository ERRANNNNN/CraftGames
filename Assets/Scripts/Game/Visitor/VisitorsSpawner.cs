using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> slots = new List<Transform>();
    [SerializeField] private Transform visitorRect;
    [SerializeField] private Transform canvas;
    [SerializeField] private float movingToPositionDelay = 0.5f;

    private List<Visitor> visitors;
    private VisitorsDishAccepter visitorsDishAccepter;
    private Vector2 startVisitorPosition = new Vector2(-207.8f, 594.0f);
    private LevelSettings levelSettings;
    private int spawnedVisitors = 0;
    private int currentDishesCount = 0;

    public void Init(LevelSettings _levelSettings)
    {
        ClearQueue();
        spawnedVisitors = 0;
        currentDishesCount = 0;
        levelSettings = _levelSettings;
        visitorsDishAccepter = new VisitorsDishAccepter(visitors);
        Visitor.OnExit += DeleteVisitorFromQueue;
        SpawnVisitors();
    }

    private void OnDestroy()
    {
        Visitor.OnExit -= DeleteVisitorFromQueue;
    }

    private void SpawnVisitors()
    {
        for (int i = 0; ((i < slots.Count) && (i < levelSettings.GetVisitorsSettings.visitorsCount)); i++)
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

    public void DeleteVisitorFromQueue(Visitor visitor)
    {
        StopCoroutine(nameof(MoveVisitorsToPositions));
        if (levelSettings.GetVisitorsSettings.visitorsCount == spawnedVisitors)
        {
            visitors.Remove(visitor);
        }
        else
        {
            visitors.Remove(visitor);
            visitors.Add(SpawnVisitor());
        }
        StartCoroutine(MoveVisitorsToPositions());
    }

    private Visitor SpawnVisitor()
    {
        Visitor visitor = Instantiate(visitorRect, canvas).GetComponent<Visitor>();
        visitor.transform.SetAsFirstSibling();

        RandomNumberGenerator.PartitionsParameters partitionsParameters = new RandomNumberGenerator.PartitionsParameters();
        partitionsParameters.InitPartitions(spawnedVisitors, levelSettings.GetVisitorsSettings.visitorsCount);
        partitionsParameters.InitValues(currentDishesCount, levelSettings.GetLevelDishesCount);
        partitionsParameters.InitPartitionsValuesLimits(
            levelSettings.GetVisitorsSettings.minVisitorDishesCount,
            levelSettings.GetVisitorsSettings.maxVisitorDishesCount
        );

        int randCount = RandomNumberGenerator.GenerateForPartitions(partitionsParameters);
        currentDishesCount += randCount;
        spawnedVisitors++;
        visitor.Init(randCount);
        return visitor;
    }

    private void ClearQueue()
    {
        if (visitors != null)
        {
            foreach (Visitor visitor in visitors)
            {
                Destroy(visitor.gameObject);
            }
        }
        visitors = new List<Visitor>();
    }
}
