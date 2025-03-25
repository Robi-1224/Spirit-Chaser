using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : Voodoo
{
    
    private void Awake()
    {
      
    }
    public void ItemBehaviour()
    {
        gameObject.transform.parent = roomManager.ritualList[0].transform;
        gameObject.transform.position = Vector3.zero;
        Debug.Log("stick");
    }
}
