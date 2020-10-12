using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : BaseObject
{

    /// <summary>
    /// Set the current value to map reference 
    /// </summary>
    /// <param name="_ref"></param>
    public override void SetMapReference(Transform _ref)
    {
        this.gameObject.name = "C" + _ref.name;
        mapPositionReference = _ref;
    }

    /// <summary>
    /// Events to be made when the click on the object occurs
    /// </summary>
    public void OnClick()
    {
        GameManager.Instance.AddToSwipe(this);
    }
}
