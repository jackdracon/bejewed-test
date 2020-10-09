using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : BaseObject
{

    public override void SetPositionByMapReference(RectTransform _ref)
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        Debug.Log("CLICKED");
    }
}
