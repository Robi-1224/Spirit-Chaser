using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    float xAngle = 180;
    public void ItemBehaviour()
    {    
        transform.eulerAngles += new Vector3(xAngle,0, 0);
    }
}
