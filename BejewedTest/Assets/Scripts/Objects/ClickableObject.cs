using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : BaseObject
{

    public override void SetMapReference(Transform _ref)
    {
        //Debug.Log("Tile Ref " + _ref.name);
        mapPositionReference = _ref;
        Debug.Log("Tile Ref " + mapPositionReference.name);
    }

    public void SetRectTransform(RectTransform _transform)
    {
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = _transform.anchoredPosition;
    }

    /// <summary>
    /// Events to be made when the click on the object occurs
    /// </summary>
    public void OnClick()
    {
        GameManager.Instance.AddToSwipe(this);
    }
}
