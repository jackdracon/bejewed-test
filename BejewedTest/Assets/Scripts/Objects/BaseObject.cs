using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseObject contains a base values that could be
/// related to use with Map coordinations
/// </summary>
public abstract class BaseObject : MonoBehaviour
{
    public Transform mapPositionReference;

    #region METHODS
    /// <summary>
    /// SetMapReference is a way to guarantee a link to a tile on the map
    /// </summary>
    /// <param name="_ref"></param>
    public virtual void SetMapReference(Transform _ref) 
    {
        
    }

    #endregion
}
