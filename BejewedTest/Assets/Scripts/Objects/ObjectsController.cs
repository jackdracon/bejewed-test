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

    /// <summary>
    /// Flag to inform if the prefab is loaded on scene
    /// </summary>
    private bool prefabLoaded = false;
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

    /// <summary>
    /// Insert a prefab object to be copied
    /// and copy it whatever is necessary to 
    /// load better and to use as a object on scene
    /// </summary>
    private void InstantiatePrefabsFirst()
    {
        objsInScene = new List<Transform>();

        foreach(GameObject _pref in prefabsToLoad)
        {
            Transform _tObj = Instantiate(_pref.transform) as Transform;
            objsInScene.Add(_tObj);
        }

        IsPrefabLoaded = true;
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
            GameObject _instance = Instantiate(instanceToCreate, _transform, false);
            //_instance.name = "C" + _transform.name;

            _instance.GetComponent<RectTransform>().localPosition = Vector2.zero;
            
            _instance.GetComponent<BaseObject>().SetMapReference(_transform);
        }
    }

    /// <summary>
    /// Get a object from prefabsToLoad's list
    /// </summary>
    /// <returns>a random object</returns>
    public GameObject GetRandomFromList()
    {
        return (objsInScene != null) ? objsInScene[Random.Range(0, (objsInScene.Count - 1))].gameObject : null;
    }

    /// <summary>
    /// Delete object on scene
    /// </summary>
    /// <param name="_obj"></param>
    public void DeleteObject(GameObject _obj)
    {
        if(_obj)
            Destroy(_obj);
    }

    /// <summary>
    /// Setter/Getter flag related to the load of prefab on scene
    /// </summary>
    public bool IsPrefabLoaded
    {
        get { return prefabLoaded; }
        private set { prefabLoaded = value; }
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
