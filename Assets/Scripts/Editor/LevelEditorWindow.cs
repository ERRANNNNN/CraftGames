using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
    }

    private Settings settings;

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
        EditorGUILayout.LabelField("Время уровня: ");
        minutes = EditorGUILayout.IntField("Минуты: ", minutes);
        seconds = EditorGUILayout.IntField("Секунды: ", seconds);
        settings.time = (minutes * 60) + seconds;

        if (GUILayout.Button("Сохранить"))
        {
            string json = JsonConvert.SerializeObject(settings);
            Debug.Log(json);
        }
    }
}
