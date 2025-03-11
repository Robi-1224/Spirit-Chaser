using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualObject : MonoBehaviour
{
    private RoomManager roomManager;

    private void Awake()
    {
        roomManager = FindAnyObjectByType<RoomManager>();
    }
    public void ItemBehaviour()
    {
        //this function gets called by the player interaction script and overwritten by the specific object scripts
        roomManager.ritualList.RemoveAt(0); 
    }
}
