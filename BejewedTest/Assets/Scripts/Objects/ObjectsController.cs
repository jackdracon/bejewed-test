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
    [Tooltip("A list with prefabs object to create")]
    public List<GameObject> prefabsToLoad;
    #endregion

    #region PUBLIC METHODS
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        Instance = this;
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
            //instanceToCreate.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 
        }
    }

    /// <summary>
    /// Get a object from prefabsToLoad's list
    /// </summary>
    /// <returns>a random object</returns>
    public GameObject GetRandomFromList()
    {
        return (prefabsToLoad != null) ? prefabsToLoad[Random.Range(0, (prefabsToLoad.Count - 1))] as GameObject : null;
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
