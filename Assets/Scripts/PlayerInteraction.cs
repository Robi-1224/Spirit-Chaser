using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject heldObject;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] GameObject inventoryHeldItem;

    private RoomManager roomManager;

    RaycastHit hit;
   

    private void Awake()
    {
       
       roomManager= FindAnyObjectByType<RoomManager>();
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 6;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
    public void interact()
    {
        errorText.gameObject.SetActive(false);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 6;
        Physics.Raycast(transform.position,forward,out hit);
        GameObject hitCollider = hit.collider.gameObject;

        if (hitCollider.CompareTag("Held item"))
        { 
            hitCollider.transform.parent = heldObject.transform;
            hitCollider.transform.localPosition = Vector3.zero;
            heldObject = hitCollider;
            inventoryHeldItem.SetActive(true);
        }
        else if (hitCollider.CompareTag("No held item ritual"))
        {
            Debug.Log("No held item ritual");
            roomManager.ritualList.Remove(hitCollider);
            ItemBehaviour();
           
        }
        else if (hitCollider.CompareTag("Held item ritual") && heldObject.tag != "Untagged")
        {
            Debug.Log("Item removed");
            roomManager.ritualList.Remove(hitCollider);
            ItemBehaviour();
        }
        else if(hitCollider.tag != "Enemy")
        {
            errorText.text = "You need the held item for that!";
            errorText.gameObject.SetActive(true);
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
