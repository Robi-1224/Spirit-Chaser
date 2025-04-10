
using TMPro;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    public GameObject heldObject;

    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] GameObject inventoryHeldItem;
    [SerializeField] int interactionRange;

    RaycastHit hit;
    protected RoomManager roomManager;

    // player sfx
    [SerializeField] AudioClip pickUpClip;

    // ritual specific sfx
    [SerializeField] AudioClip candleSFX;

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
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Physics.Raycast(transform.position,forward,out hit,interactionRange);
        GameObject hitCollider = hit.collider.gameObject;

        if (hitCollider.CompareTag("Held item"))
        {
            // moves the held item into the heldItem transform of the player and activates it in the inventory
            roomManager.audioSource.PlayOneShot(pickUpClip,1f);
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
            // if the player tries to interact with the ritual object without held object the error text displays
            errorText.gameObject.SetActive(true);
        }
    }

    private void ItemBehaviour()
    {
        // checks for the name of the ritual object when interacted with and gets the itembehaviour of that object, and can also set the position of the held item there
        GameObject hitCollider = hit.collider.gameObject;
        switch(hitCollider.name)
        {
            case "Candle": hitCollider.GetComponent<Candle>().ItemBehaviour(); roomManager.audioSource.PlayOneShot(candleSFX, 1f); return;

            case "Cross": hitCollider.GetComponent<Cross>().ItemBehaviour(); return;

            case "Voodoo": hitCollider.GetComponent<Voodoo>().ItemBehaviour(); inventoryHeldItem.SetActive(false); return;

            case "Torri": heldObject.transform.position = hitCollider.transform.position; hitCollider.GetComponent<Tag>().ItemBehaviour(); inventoryHeldItem.SetActive(false);  return;

            case "Demon": hitCollider.GetComponentInParent<Demon>().DamageRecieved(); inventoryHeldItem.SetActive(false); break;
        }
    }

   
}
