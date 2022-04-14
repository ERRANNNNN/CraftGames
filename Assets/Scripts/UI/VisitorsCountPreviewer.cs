using TMPro;
using UnityEngine;

public class VisitorsCountPreviewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI allVisitorsCountTextMesh;
    [SerializeField] private TextMeshProUGUI remainingVisitorsCountTextMesh;

    public void Init(int allVisitorsCount)
    {
        allVisitorsCountTextMesh.text = allVisitorsCount.ToString();
        remainingVisitorsCountTextMesh.text = allVisitorsCount.ToString();
    }

    public void UpdateRemainingVisitorsCount(int remainingVisitorsCount)
    {
        remainingVisitorsCountTextMesh.text = remainingVisitorsCount.ToString();
    }
}
