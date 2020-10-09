using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseObject : MonoBehaviour
{
    private RectTransform mapPositionReference;

    #region METHODS
    public virtual void SetPositionByMapReference(RectTransform _ref) { }

    public virtual RectTransform GetTileReference 
    {
        get { return mapPositionReference; }
    }
    #endregion
}
