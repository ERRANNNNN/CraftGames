using System.IO;
using UnityEngine;

public class LevelSettingsLoaderFromAssets : IJsonDataLoader
{
    public string LoadJsonData(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            return reader.ReadLine();
        }
    }
}
