using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : RitualObject
{
    // Start is called before the first frame update
    public override void ItemBehaviour()
    {
        var fire = GetComponentInChildren<GameObject>();
        fire.SetActive(true);
        Debug.Log("Candle override");
        
    }
}
