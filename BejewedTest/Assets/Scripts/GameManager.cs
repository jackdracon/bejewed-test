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

    /// <summary>
    /// Add object to a list that will validate 
    /// the swipe between the objects
    /// </summary>
    /// <param name="_obj"></param>
    public void AddToSwipe(ClickableObject _obj)
    {
        toSwipe.Add(_obj);
        Swipe();
    }

    /// <summary>
    /// Clean the objects on list to swipe
    /// </summary>
    public void CleanObjectsToSwipe()
    {
        toSwipe.Clear();
    }

    /// <summary>
    /// Swipe between the objects that was selected
    /// </summary>
    public void Swipe() 
    {
        if(toSwipe.Count == 2)
        {
            ClickableObject swipe1 = toSwipe[0], swipe2 = toSwipe[1];
            if(swipe1.name != swipe2.name)
            {
                bool _lineValid = IsLinePairAdjacent(swipe1.name, swipe2.name);
                bool _columnValid = IsColumnPairAdjacent(swipe1.name, swipe2.name);

                Debug.Log("Line - " + _lineValid + " @ Col - " + _columnValid);
                
                    Transform _firstObj = swipe1.mapPositionReference;
                    Transform _secObj = swipe2.mapPositionReference;

                    //set reference
                    toSwipe[0].transform.SetParent(_secObj, false);
                    toSwipe[1].transform.SetParent(_firstObj, false);
                    toSwipe[0].SetMapReference(_secObj);
                    toSwipe[1].SetMapReference(_firstObj);

                    Debug.Log("Nice Swipe");
            }
            CleanObjectsToSwipe();
        }
    }

    /// <summary>
    /// Validate the objects if they are adjacent 
    /// </summary>
    /// <param name="_nameObj1">first object selected</param>
    /// <param name="_nameObj2">second object selected</param>
    /// <returns>if they are adjacent, its a valid moviment </returns>
    private bool IsLinePairAdjacent(string _nameObj1, string _nameObj2)
    {
        string _prefixName = "Ctile";
        string[] _cleanedName = new string[] { _nameObj1.Remove(0, _prefixName.Length), 
                                               _nameObj2.Remove(0, _prefixName.Length) };
        
        int _l1Var = (int)char.GetNumericValue(_cleanedName[0][1]);
        int _l2Var = (int)char.GetNumericValue(_cleanedName[1][1]);

        Debug.Log("OBJ 1 L - " + _l1Var + "| OBJ 2 L - " + _l2Var);

        if (_l1Var == _l2Var)
            return true;

        return false;
    }

    private bool IsColumnPairAdjacent(string _nameObj1, string _nameObj2)
    {
        string _prefixName = "Ctile";
        string[] _cleanedName = new string[] { _nameObj1.Remove(0, _prefixName.Length),
                                               _nameObj2.Remove(0, _prefixName.Length) };

        int _c1 = (int)char.GetNumericValue(_cleanedName[0][(_cleanedName[0].Length -1)]);
        int _c2 = (int)char.GetNumericValue(_cleanedName[1][(_cleanedName[1].Length - 1)]);

        Debug.Log("OBJ 1 C - " + _c1 + "| OBJ 2 C - " + _c2);

        if (_c1 == _c2)
            return true;

        return false;
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
