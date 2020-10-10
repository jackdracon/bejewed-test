using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// GameManager is responsable to control the 
/// game logic and MapManager
/// </summary>
public class GameManager : MonoBehaviour
{
    //GameManager instance
    private static GameManager instance;

    //A array with the current object to swipe
    private List<ClickableObject> toSwipe = new List<ClickableObject>(2);

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void AddToSwipe(ClickableObject _obj)
    {
        Debug.Log("ADD TO SWIPE " + _obj.mapPositionReference);
        toSwipe.Add(_obj);
        Swipe();
    }

    public void CleanObjectsToSwipe()
    {
        toSwipe.Clear();
    }

    public void Swipe() 
    {
        if(toSwipe.Count == 2)
        {
            ClickableObject swipe1 = toSwipe[0], swipe2 = toSwipe[1];
            Transform _firstObj = swipe1.mapPositionReference;
            Transform _secObj = swipe2.mapPositionReference;

            //set reference
            toSwipe[0].transform.SetParent(_secObj,false);
            toSwipe[1].transform.SetParent(_firstObj, false);
            toSwipe[0].SetMapReference(_secObj);
            toSwipe[1].SetMapReference(_firstObj);

            Debug.Log("Swipe");
            CleanObjectsToSwipe();
        }
    }

    /// <summary>
    /// GameManager's Instance to control the logic and 
    /// </summary>
    public static GameManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
}
