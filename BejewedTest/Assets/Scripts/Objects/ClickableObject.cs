using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : BaseObject
{
    //private void OnEnable()
    //{
    //    //RectTransform _transform = this.transform.gameObject.GetComponentInParent<RectTransform>();
    //    //Debug.Log("Parent " + GetTileReference.gameObject.name);
    //}

    public override void SetMapReference(Transform _ref)
    {
        Debug.Log("Tile Ref " + _ref.name);
        base.SetMapReference(_ref);
        //SetNewPosition(_diff);
    }

    /// <summary>
    /// Events to be made when the click on the object occurs
    /// </summary>
    public void OnClick()
    {
        GameManager.Instance.AddToSwipe(this);
    }

    private void SetNewPosition(Vector2 _newPos)
    {

        //this.gameObject.GetComponent<RectTransform>().anchoredPosition = _newPos;
    }
}
