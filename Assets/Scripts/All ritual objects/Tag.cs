using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : Voodoo
{
    public override void ItemBehaviour()
    {
        playerInteraction.heldObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        playerInteraction.heldObject.transform.parent = null;
        playerInteraction.heldObject = heldObject;
        roomManager.ritualList.Remove(gameObject);
    }
}
