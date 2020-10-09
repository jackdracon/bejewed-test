using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
MapManager is responsable for list the tiles object
*/
public class MapManager : MonoBehaviour
{
    //A GameObject collection with the objects on scene 
    private GameObject[] tileCollection;

    [Tooltip("Tag value related for research on the scene"), SerializeField]
    private string tagTileName = "Tile";

    private static MapManager instance;

    //Static Instance for as simple Singleton structure
    public static MapManager Instance { get => instance; set => instance = value; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(Instance);
            Instance = this;
        }

        GameObject[] _tilesOnScene  = GameObject.FindGameObjectsWithTag(tagTileName);
        tileCollection = new GameObject[_tilesOnScene.Length];
        tileCollection = _tilesOnScene;

        Debug.Log("Found: " + tileCollection.Length);
    }

    private void Start()
    {
        InsertObjects();
    }

    /// <summary>
    /// InsertObjects
    /// </summary>
    public void InsertObjects()
    {
        foreach (GameObject _obj in GetCurrentTiles())
        {
            ObjectsController.Instance.CreateObject(_obj.transform);
        }
    }

    //Get the current tiles on scene
    public GameObject[] GetCurrentTiles()
    {
        return tileCollection;
    }
}