﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
MapManager is responsable for list the tiles object
*/
public class MapManager : MonoBehaviour
{
    #region VARIABLES
    //The map limit values
    const int rowMax = 8, column = rowMax;

    //A GameObject collection with the objects on scene 
    private GameObject[] tileCollection;

    [Tooltip("Tag value related for research on the scene"), SerializeField]
    private string tagTileName = "Tile";

    [Tooltip("The distance between the center tiles")]
    private float distanceBetweenTiles = 110;

    //instance object
    private static MapManager instance;
    #endregion

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
        AddObjectsTilemap();
    }

    /// <summary>
    /// Insert a object as child from tile on map
    /// </summary>
    public void AddObjectsTilemap()
    {
        foreach (GameObject _obj in GetCurrentTiles())
        {
            ObjectsController.Instance.CreateObject(_obj.transform);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddObjectToColumn()
    {

    }

    //Get the current tiles on scene
    public GameObject[] GetCurrentTiles()
    {
        return tileCollection;
    }
}