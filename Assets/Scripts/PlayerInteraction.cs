using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : RitualObject
{
    [SerializeField] GameObject heldObject;

    public void Interaction(GameObject other)
    {
      

       if (other.CompareTag("Held item"))
       {
            heldObject = other.gameObject;
       }
       else if(other.CompareTag("No held item ritual"))
       {
            ItemBehaviour();
       }
       else if(other.CompareTag("Held item ritual") && heldObject!= null)
       {
            ItemBehaviour();
       }
       else
       {
            Debug.Log("Need the held item");
       }

      
    }

    private void OnTriggerEnter(Collider other)
    {
       Interaction(other.gameObject);
    }
}
