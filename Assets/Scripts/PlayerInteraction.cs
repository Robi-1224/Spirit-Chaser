using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject heldObject;

    private RoomManager roomManager;

    RaycastHit hit;
   

    private void Awake()
    {
       
       roomManager= FindAnyObjectByType<RoomManager>();
    }
    public void interact()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 6;
        Physics.Raycast(transform.position,forward,out hit);
        GameObject hitCollider = hit.collider.gameObject;

        if (hitCollider.CompareTag("Held item"))
        { 
            hitCollider.transform.parent = heldObject.transform;
            hitCollider.transform.localPosition = Vector3.zero;
            heldObject = hitCollider;
        }
        else if (hitCollider.CompareTag("No held item ritual"))
        {
            Debug.Log("No held item ritual");
            roomManager.ritualList.Remove(hit.collider.gameObject);
            ItemBehaviour();
           
        }
        else if (hitCollider.CompareTag("Held item ritual") && heldObject.tag != "Untagged")
        {
            Debug.Log("Item removed");
            roomManager.ritualList.Remove(hit.collider.gameObject);
            ItemBehaviour();
        }
        else
        {
            Debug.Log("Need the held item");
        }
    }

    private void ItemBehaviour()
    {
        GameObject hitCollider = hit.collider.gameObject;
        switch(hitCollider.name)
        {
            case "Candle": hitCollider.GetComponent<Candle>().ItemBehaviour(); return;
        }
    }

   
}
