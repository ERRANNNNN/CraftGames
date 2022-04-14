using UnityEngine;
using Newtonsoft.Json;
public class LevelSettings
{
    public struct VisitorsSettings
    {
        public VisitorsSettings(int _visitorsCount, int _visitorsWalkDuration, int _minVisitorDishesCount, int _maxVisitorDishesCount)
        {
            visitorsCount = _visitorsCount;
            visitorsWalkDuration= _visitorsWalkDuration;
            maxVisitorDishesCount = _maxVisitorDishesCount;
            minVisitorDishesCount = _minVisitorDishesCount;
        }

        public int visitorsCount;
        public int visitorsWalkDuration;
        public int maxVisitorDishesCount;
        public int minVisitorDishesCount;
    }

    struct JsonSettings
    {
        public int visitorsCount;
        public int dishesCount;
        public int time;
        public int maxDishesInOrder;
        public int boosterCount;
    }

    private VisitorsSettings visitorsSettings;

    public VisitorsSettings GetVisitorsSettings => visitorsSettings;

    private int levelDishesCount;
    public int GetLevelDishesCount => levelDishesCount;

    private int levelTime;
    public int LevelTime => levelTime;

    public LevelSettings()
    {
        IJsonDataLoader loader = new LevelSettingsLoaderFromAssets();
        LoadSettings(loader);
    }

    private void LoadSettings(IJsonDataLoader loader)
    {
        string json = loader.LoadJsonData(Application.dataPath + "/StreamingAssets/Level.json");
        JsonSettings settings = JsonConvert.DeserializeObject<JsonSettings>(json);
        levelTime = settings.time;
        visitorsSettings = new VisitorsSettings(settings.visitorsCount, 1, 1, settings.maxDishesInOrder);
        levelDishesCount = settings.dishesCount;
        levelTime = settings.time;
    }
}
