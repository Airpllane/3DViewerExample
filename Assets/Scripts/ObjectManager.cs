using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    string serializePath => Path.Combine(Application.persistentDataPath, "SerializedData");
    string serializeFilePath => Path.Combine(serializePath, "objects.json");

    public List<ViewableObject> objectList;

    public void Serialize()
    {
        string json = JsonUtility.ToJson(new ObjectJSONContainer(objectList));

        if (!Directory.Exists(serializePath))
        {
            Directory.CreateDirectory(serializePath);
        }

        File.WriteAllText(serializeFilePath, json);
    }

    public void Deserialize()
    {
        try
        {
            string json = File.ReadAllText(serializeFilePath);
            this.objectList = JsonUtility.FromJson<ObjectJSONContainer>(json).objectList;
        }
        catch
        {
            // example JSON
            string json = "{\"objectList\":[{\"objectName\":\"Green Box\",\"Mesh\":0,\"Material\":0,\"scale\":0.5,\"BaseColor\":3,\"alpha\":1.0},{\"objectName\":\"Blue Sphere\",\"Mesh\":1,\"Material\":2,\"scale\":0.3,\"BaseColor\":4,\"alpha\":0.3},{\"objectName\":\"Red Pill\",\"Mesh\":2,\"Material\":0,\"scale\":0.1,\"BaseColor\":2,\"alpha\":1.0}]}";
            this.objectList = JsonUtility.FromJson<ObjectJSONContainer>(json).objectList;
        }
    }

    private void Awake()
    {
        Deserialize();
    }

    private void OnApplicationQuit()
    {
        Serialize();
    }
}

[Serializable]
class ObjectJSONContainer
{
    public List<ViewableObject> objectList;

    public ObjectJSONContainer(List<ViewableObject> objectList)
    {
        this.objectList = objectList;
    }
}