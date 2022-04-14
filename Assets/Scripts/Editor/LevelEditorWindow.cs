using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System;
using System.IO;

public class LevelEditorWindow : EditorWindow
{
    public int minutes;
    public int seconds;
    
    [Serializable]
    struct Settings
    {
        public int visitorsCount;
        public int dishesCount;
        public int time;
        public int maxDishesInOrder;
        public int boosterCount;
    }

    private Settings settings;
    private string timeString;

    [MenuItem("Window/Level Editor")]
    public static void OpenWindow()
    {
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));
        window.minSize = new Vector2(300, 600);
        window.Show();
    }

    private void OnGUI()
    {
        settings.visitorsCount = EditorGUILayout.IntField("Кол-во посетителей: ", settings.visitorsCount);
        settings.dishesCount = EditorGUILayout.IntField("Кол-во блюд: ", settings.dishesCount);
        settings.maxDishesInOrder = EditorGUILayout.IntField("Макс. кол-во блюд в заказе: ", settings.maxDishesInOrder);
        EditorGUILayout.Separator();

        settings.boosterCount = EditorGUILayout.IntField("Количество бустеров: ", settings.boosterCount);
        EditorGUILayout.Separator();
        timeString = EditorGUILayout.TextField("Время (чч:мм:сс): ", timeString);
        if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
        {
            settings.time = Convert.ToInt32(timeSpan.TotalSeconds);
        }

        if (GUILayout.Button("Сохранить"))
        {
            bool error = false;
            if ((settings.visitorsCount * settings.maxDishesInOrder) < settings.dishesCount)
            {
                Debug.LogWarning("Максимальное количество блюд не может быть больше чем (кол-во посетителей * макс. кол-во блюд на 1 посетителя)");
                error = true;
            }
            if (settings.dishesCount < settings.visitorsCount) {
                Debug.LogWarning("Максимальное количество блюд не может быть меньше чем по одному на посетителя");
                error = true;
            }
            if (!error)
            {
                string json = JsonConvert.SerializeObject(settings);
                string path = Application.dataPath + "/StreamingAssets/Level.json";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine(json);
                }
                Debug.Log("Настройки уровня сохранены");
            }
        }
        Repaint();
    }
}
