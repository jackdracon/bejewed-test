using System;
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

    //Line value from selected objects
    private int l_obj1 = -1, l_obj2 = -1;
    //column value from selected objects 
    private int c_obj1 = -1, c_obj2 = -1;

    //A array with the current object to swipe
    private List<ClickableObject> toSwipe = new List<ClickableObject>(2);

    //Action to be called to search the combinations
    public Action SearchToCombinations;

    private Transform prevObj1, prevObj2;

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
            
            //Check if are different objects
            if(swipe1.name != swipe2.name)
            {
                //moviment is valids
                bool _lineValid = SameLine(swipe1.name, swipe2.name);
                bool _columnValid = SameColumn(swipe1.name, swipe2.name);

                //Adjascent
                bool _isLineAdjascent = LineAdjascent();
                bool _isColumnAdjascent = ColumnAdjascent();

                Debug.Log("Line - " + _lineValid + " @ Col - " + _columnValid + "|| Adjascent @ L " + _isLineAdjascent + " - C " + _isColumnAdjascent);
                if(_isLineAdjascent || _isColumnAdjascent)
                {
                    Transform _firstObj = swipe1.mapPositionReference;
                    Transform _secObj = swipe2.mapPositionReference;

                    //swipe the parents
                    toSwipe[0].transform.SetParent(_secObj, false);
                    toSwipe[1].transform.SetParent(_firstObj, false);

                    //swipe the map references
                    toSwipe[0].SetMapReference(_secObj);
                    toSwipe[1].SetMapReference(_firstObj);

                    SearchToCombinations();
                }
            }
            CleanObjectsToSwipe();
        }
    }

    //In successful combinations, that will destroy the objects and and call to create others
    public void SuccessCombination(List<GameObject> _sameTypeObjs)
    {
        List<Transform> _parentsToCreate = new List<Transform>();
        
        Debug.Log("SUCCESS");

        //Destroy objects
        foreach (GameObject _obj in _sameTypeObjs)
        {
            Transform _base = _obj.GetComponent<BaseObject>().mapPositionReference;
            _parentsToCreate.Add(_base);
            ObjectsController.Instance.DeleteObject(_obj);
        }

        //create another objects
        foreach (Transform _obj in _parentsToCreate)
        {
            StartCoroutine(ObjectsController.Instance.DelayedCreateObject(_obj));
        }
    }

    //Failed combinations
    public void FailedCombination()
    {
        //Desfazer a jogada
    }

    /// <summary>
    /// Validate if they are line adjascent 
    /// </summary>
    /// <returns>true, if they are adjascent</returns>
    private bool LineAdjascent()
    {
        if ((l_obj1 - 1) == l_obj2 || 
            (l_obj1 + 1) == l_obj2)
            return true;
        return false;
    }

    /// <summary>
    /// Validate if they are line adjascent 
    /// </summary>
    /// <returns>true, if they are adjascent</returns>
    private bool ColumnAdjascent()
    {
        if ((c_obj1 + 1) == c_obj2 ||
            (c_obj1 - 1) == c_obj2)
            return true;
        return false;
    }

    /// <summary>
    /// Object's validation if they are line adjacent 
    /// </summary>
    /// <param name="_nameObj1">first object selected</param>
    /// <param name="_nameObj2">second object selected</param>
    /// <returns>true, if they are on the same line. False, if not </returns>
    private bool SameLine(string _nameObj1, string _nameObj2)
    {
        string _prefixName = "Ctile";
        string[] _cleanedName = new string[] { _nameObj1.Remove(0, _prefixName.Length), 
                                               _nameObj2.Remove(0, _prefixName.Length) };

        l_obj1 = (int)char.GetNumericValue(_cleanedName[0][1]);
        l_obj2 = (int)char.GetNumericValue(_cleanedName[1][1]);

        Debug.Log("OBJ 1 L - " + l_obj1 + "| OBJ 2 L - " + l_obj2);

        if (l_obj1 == l_obj2)
            return true;

        return false;
    }

    /// <summary>
    /// Object's validation if they are column adjacent
    /// </summary>
    /// <param name="_nameObj1">first object selected</param>
    /// <param name="_nameObj2">second object selected</param>
    /// <returns>true, if they are on the same column. False, if not </returns>
    private bool SameColumn(string _nameObj1, string _nameObj2)
    {
        string _prefixName = "Ctile";
        string[] _cleanedName = new string[] { _nameObj1.Remove(0, _prefixName.Length),
                                               _nameObj2.Remove(0, _prefixName.Length) };

        c_obj1 = (int)char.GetNumericValue(_cleanedName[0][(_cleanedName[0].Length -1)]);
        c_obj2 = (int)char.GetNumericValue(_cleanedName[1][(_cleanedName[1].Length - 1)]);

        Debug.Log("OBJ 1 C - " + c_obj1 + "| OBJ 2 C - " + c_obj2);

        if (c_obj1 == c_obj2)
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
