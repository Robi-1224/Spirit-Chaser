using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject heldObject;

    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] GameObject inventoryHeldItem;
    [SerializeField] int interactionRange;

    RaycastHit hit;
    protected RoomManager roomManager;

    private void Awake()
    { 
       roomManager= FindAnyObjectByType<RoomManager>();
    }
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * interactionRange;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
    public void interact()
    {
        errorText.gameObject.SetActive(false);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * interactionRange;
        Physics.Raycast(transform.position,forward,out hit);
        GameObject hitCollider = hit.collider.gameObject;

        if (hitCollider.CompareTag("Held item"))
        { 
            hitCollider.transform.parent = heldObject.transform;
            hitCollider.transform.localPosition = Vector3.zero;
            heldObject = hitCollider;
            inventoryHeldItem.SetActive(true);
        }
        else if (hitCollider.CompareTag("Held item ritual") && heldObject.tag != "Untagged" || hitCollider.CompareTag("No held item ritual"))
        {
            roomManager.ritualList.Remove(hitCollider);
            ItemBehaviour();
        }
        else if(hitCollider.CompareTag("Held item ritual") && heldObject.tag == "Untagged")
        {
            errorText.gameObject.SetActive(true);
        }
    }

    private void ItemBehaviour()
    {
        GameObject hitCollider = hit.collider.gameObject;
        switch(hitCollider.name)
        {
            case "Candle": hitCollider.GetComponent<Candle>().ItemBehaviour(); return;

            case "Cross": hitCollider.GetComponent<Cross>().ItemBehaviour(); return;

            case "Voodoo": hitCollider.GetComponent<Voodoo>().ItemBehaviour(); return;
        }
    }

   
}
