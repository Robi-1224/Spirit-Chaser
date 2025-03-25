using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voodoo : MonoBehaviour
{
    private PlayerInteraction playerInteraction;
    private RoomManager roomManager;
    private void Awake()
    {
        playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        roomManager = FindAnyObjectByType<RoomManager>();
    }
    public void ItemBehaviour()
    {
        playerInteraction.heldObject.transform.parent = roomManager.ritualList[0].transform;
        playerInteraction.heldObject= null;
        roomManager.ritualList.RemoveAt(0);
    }
}
