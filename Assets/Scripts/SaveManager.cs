using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveManager : MonoBehaviour
{

    [SerializeField] Tilemap groundTileMap;
    [SerializeField] Tilemap environmentTileMap;
    [SerializeField] TileBase tile;

    private void Awake()
    {   
        SingletonSaveManager();
        CreateFolder();
    }

    private void Update()
    {
        if (groundTileMap == null)
            groundTileMap = GameObject.FindGameObjectWithTag("Ground").ConvertTo<Tilemap>();
        if (environmentTileMap == null)
            environmentTileMap = GameObject.FindGameObjectWithTag("Environment").ConvertTo<Tilemap>();


    }

    void CreateFolder()
    {
        Directory.CreateDirectory("C:/Users/borae/AppData/LocalLow/Bora/GameFac/saves");
    }

    public void SaveGroundData()
    {
        GroundConverter converter = new GroundConverter();
        converter.convertData.Clear();
        for (int i = groundTileMap.cellBounds.min.x; i < groundTileMap.cellBounds.max.x; i++)
        {
            for (int j = groundTileMap.cellBounds.min.y; j < groundTileMap.cellBounds.max.y; j++)
            {
                TileBase tempTile = groundTileMap.GetTile(new Vector3Int(i, j, 0));
                if (tempTile != null)
                {
                    converter.convertData.Add(new ConvertData(i, j, 0, tempTile));
                }
            }
        }

        string json = JsonUtility.ToJson(converter, true);
        File.WriteAllText(Application.persistentDataPath + "/saves/groundSave.json", json);
    }

    public void SaveEnvironmentData()
    {
        EnvironmentConverter converter = new EnvironmentConverter();
        converter.convertData.Clear();
        for (int i = environmentTileMap.cellBounds.min.x; i < environmentTileMap.cellBounds.max.x; i++)
        {
            for (int j = environmentTileMap.cellBounds.min.y; j < environmentTileMap.cellBounds.max.y; j++)
            {
                TileBase tempTile = environmentTileMap.GetTile(new Vector3Int(i, j, 0));
                if (tempTile != null)
                {
                    converter.convertData.Add(new ConvertData(i, j, 0, tempTile));
                }
            }
        }
        string json = JsonUtility.ToJson(converter,true);
        File.WriteAllText(Application.persistentDataPath + "/saves/environmentSave.json", json);
    }

    public void LoadEnvironmentData()
    {
        EnvironmentConverter conventer = new EnvironmentConverter();
        conventer.convertData.Clear();
        if (File.Exists(Application.persistentDataPath + "/saves/environmentSave.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/saves/environmentSave.json");
            conventer = JsonUtility.FromJson<EnvironmentConverter>(json);
            for (int i = 0; i < conventer.convertData.Count; i++)
            {
                environmentTileMap.SetTile(new Vector3Int((int)conventer.convertData[i].x, (int)conventer.convertData[i].y, 0)
                   ,conventer.convertData[i].tile);
            }

        }
    }

    public void LoadGroundData()
    {
        GroundConverter converter = new GroundConverter();
        if (File.Exists(Application.persistentDataPath + "/saves/groundSave.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/saves/groundSave.json");
            converter = JsonUtility.FromJson<GroundConverter>(json);
            groundTileMap.ClearAllTiles();
            for (int i = 0; i < converter.convertData.Count; i++)
            {
                groundTileMap.SetTile(new Vector3Int((int)converter.convertData[i].x,
                    (int)converter.convertData[i].y, 0), converter.convertData[i].tile);
            }
        }
    }


    void SingletonSaveManager()
    {
        if (FindObjectsOfType<SaveManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


}

[Serializable]
public class ConvertData
{
    public float x;
    public float y;
    public float z;
    public TileBase tile;

    public ConvertData(float x, float y, float z, TileBase tile)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.tile = tile;
    }
}


[Serializable]
public class GroundConverter
{
    public List<ConvertData> convertData = new List<ConvertData>();

}


[Serializable]
public class EnvironmentConverter
{
    public List<ConvertData> convertData = new List<ConvertData>();
}




