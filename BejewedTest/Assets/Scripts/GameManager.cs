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
        Debug.Log("ADDD TO SWIPE");
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
            //RectTransform _firstObjectPos = toSwipe[0].GetTileReference;
            //RectTransform _secondObjectPos = toSwipe[1].GetTileReference;
            //toSwipe[0].SetMapReference(_secondObjectPos);
            //toSwipe[1].SetMapReference(_firstObjectPos);
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
