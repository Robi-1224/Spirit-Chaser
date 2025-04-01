using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voodoo : MonoBehaviour
{
    protected PlayerInteraction playerInteraction;
    protected RoomManager roomManager;
    protected GameObject heldObject;
    private void Awake()
    {
        playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        roomManager = FindAnyObjectByType<RoomManager>();
        heldObject = playerInteraction.heldObject;
    }
    public virtual void ItemBehaviour()
    {
        playerInteraction.heldObject.transform.position = roomManager.ritualList[0].transform.position;
        playerInteraction.heldObject.transform.rotation = roomManager.ritualList[0].transform.rotation;

        playerInteraction.heldObject.transform.parent = null;
        playerInteraction.heldObject= heldObject;
        roomManager.ritualList.RemoveAt(0);
    }
}
