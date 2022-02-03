
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class JsonTesting : MonoBehaviour
{
    private static string json;
    void Start()
    {
        json = File.ReadAllText(Application.dataPath + "/Resources/data/JsonFile.json");
        JsonUtility_Parse();
    }
    private void JsonUtility_Parse()
    {
        ListItem items = JsonUtility.FromJson<ListItem>(json);

        Debug.Log(items.Values.Length);
        for (int i = 0; i < items.Values.Length; i++)
        {
            Debug.Log(items.Values[i].Text);
        }
    }
    // Given:
    // playerName = "Dr Charles"
    // lives = 3
    // health = 0.8f
    // SaveToString returns:
    // {"playerName":"Dr Charles","lives":3,"health":0.8}
}
[Serializable]
public class ListItem
{
    public Values[] Values;
}
[Serializable]
public class Values
{
    public string Text;
}