using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    #region VARIABLES
    //ObjectsControllet instance
    private static ObjectsController instance;

    [Tooltip("Object's Parent to instantiate"), SerializeField]
    private Transform objsParent;

    [Space]
    [Tooltip("A list with prefabs object to create")]
    public List<GameObject> prefabsToLoad;
    #endregion

    //Objects that is on scene to be re-created whatever is needed
    private List<Transform> objsInScene;
    #region PUBLIC METHODS
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        Instance = this;

        InstantiatePrefabsFirst();
    }

    private void InstantiatePrefabsFirst()
    {
        objsInScene = new List<Transform>();
        foreach(GameObject _pref in prefabsToLoad)
        {
            Transform _tObj = Instantiate(_pref.transform) as Transform;
            objsInScene.Add(_tObj);
        }
    }

    /// <summary>
    /// Create a specific object using the position from transform
    /// </summary>
    /// <param name="_transform"></param>
    public void CreateObject(Transform _transform)
    {
        GameObject instanceToCreate = GetRandomFromList();
        if (instanceToCreate)
        {
            Instantiate(instanceToCreate, _transform, false);
            instanceToCreate.name = "C" + _transform.gameObject.name;
            instanceToCreate.GetComponent<RectTransform>().localPosition = Vector2.zero;
            //Debug.Log("Tile Ref " + _transform.name);
            instanceToCreate.GetComponent<BaseObject>().SetMapReference(_transform);
        }
    }

    /// <summary>
    /// Get a object from prefabsToLoad's list
    /// </summary>
    /// <returns>a random object</returns>
    public GameObject GetRandomFromList()
    {
        return (objsInScene != null) ? objsInScene[Random.Range(0, (objsInScene.Count - 1))].gameObject : null;
        //return (prefabsToLoad != null) ? prefabsToLoad[Random.Range(0, (prefabsToLoad.Count - 1))] as GameObject : null;
    }

    /// <summary>
    /// Delete object
    /// </summary>
    /// <param name="_obj"></param>
    public void DeleteObject(GameObject _obj)
    {
        if(_obj)
            Destroy(_obj);
    }

    /// <summary>
    /// Getter/Setter Instance
    /// </summary>
    public static ObjectsController Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    #endregion
}
