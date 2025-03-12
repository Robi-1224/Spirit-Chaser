using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private MeshRenderer fire;
    // Start is called before the first frame update

    private void Awake()
    {
        //transform.find found through an online post
        fire = transform.Find("Child").GetComponent<MeshRenderer>();
    }
    public void ItemBehaviour()
    {
        fire.enabled = true;
        Debug.Log("Candle is enabled");
    }
}
