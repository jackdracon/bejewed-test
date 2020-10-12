using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseObject contains a base values that could be
/// related to use with Map coordinations
/// </summary>
public abstract class BaseObject : MonoBehaviour
{
    #region VARIABLES
    [Tooltip("Object's type"), SerializeField]
    private OBJECTTYPE type = OBJECTTYPE.DIAMOND;

    //Map Reference on the map
    public Transform mapPositionReference;
    #endregion

    #region METHODS
    /// <summary>
    /// SetMapReference is a way to guarantee a link to a tile on the map
    /// </summary>
    /// <param name="_ref"></param>
    public virtual void SetMapReference(Transform _ref)  { }

    //Object's type
    public OBJECTTYPE MyObjectType
    {
        get { return type; }
    }

    #endregion
}

//Object type
public enum OBJECTTYPE 
{ 
    QUAD = 0,
    TRIANGLE = 1,
    DIAMOND = 2,
    ELLIPSE = 3,
    HEX = 4
}
