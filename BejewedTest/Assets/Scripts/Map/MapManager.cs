using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
MapManager is responsable for list the tiles object
*/
public class MapManager : MonoBehaviour
{
    #region VARIABLES
    //The map limit values
    const int lineMax = 8, columnMax = lineMax;

    //A GameObject collection with the objects on scene 
    private GameObject[] tileCollection;

    [Tooltip("Tag value related for research on the scene"), SerializeField]
    private string tagTileName = "Tile";

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
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => ObjectsController.Instance.IsPrefabLoaded == true);
        Debug.Log("Prefab " + ObjectsController.Instance.IsPrefabLoaded);
        AddObjectsTilemap();
        GameManager.Instance.SearchToCombinations += MapSearchCombination;
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

    //Search for objects of the same type to make combinations
    public void MapSearchCombination()
    {
        //Verificar por combinações na linha
        List<GameObject> _tempList = new List<GameObject>();
        List<GameObject> _tempTypeFound = new List<GameObject>();

        string _baseName = "tile";
        for (uint _indLine = 0; _indLine < lineMax; _indLine++)
        {
            for (uint _indColumn = 0; _indColumn < columnMax; _indColumn++)
            {
                //separar os objetos por linha
                foreach (GameObject _gO in GetCurrentTiles())
                {
                    string _nameToFound = _baseName + "l" + _indLine + "c" + _indColumn;
                    if (_gO.name == _nameToFound)
                    {
                        _tempList.Add(_gO);
                        BaseObject _child = _gO.GetComponentInChildren<BaseObject>();
                        if (_child)
                        {
                            if (_tempTypeFound.Count > 0)
                            {
                                if (_tempTypeFound[0].GetComponentInChildren<BaseObject>().MyObjectType == _child.MyObjectType)
                                {
                                    _tempTypeFound.Add(_child.gameObject);
                                }
                                else
                                {
                                    _tempTypeFound.Clear();
                                    _tempTypeFound.Add(_child.gameObject);
                                }
                            }
                            else
                                _tempTypeFound.Add(_child.gameObject);
                        }
                    }
                }

                //Validate the combinations
                if (_tempTypeFound.Count > 2)
                {
                    GameManager.Instance.SuccessCombination(_tempTypeFound);
                }
                else
                {
                    GameManager.Instance.FailedCombination();
                }
            }
        }
    }

    //Get the current tiles on scene
    public GameObject[] GetCurrentTiles()
    {
        return tileCollection;
    }

    ///Quit application 
    private void OnApplicationQuit()
    {
        GameManager.Instance.SearchToCombinations -= MapSearchCombination;
    }

}