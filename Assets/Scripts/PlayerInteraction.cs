
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
    private AudioSource audioSource;

    // player sfx
    [SerializeField] AudioClip pickUpClip;

    //ritual sfx
    [SerializeField] AudioClip ritualSFX;

    private void Awake()
    { 
       roomManager= FindAnyObjectByType<RoomManager>();
       audioSource = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(pickUpClip,2f);
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
            case "Candle": hitCollider.GetComponent<Candle>().ItemBehaviour();audioSource.PlayOneShot(ritualSFX, 1.3f); return;

            case "Cross": hitCollider.GetComponent<Cross>().ItemBehaviour();audioSource.PlayOneShot(ritualSFX, 1.3f); return;

            case "Voodoo": hitCollider.GetComponent<Voodoo>().ItemBehaviour(); inventoryHeldItem.SetActive(false);audioSource.PlayOneShot(ritualSFX, 1.3f); return;

            case "Torri": heldObject.transform.position = hitCollider.transform.position; hitCollider.GetComponent<Tag>().ItemBehaviour(); audioSource.PlayOneShot(ritualSFX, 2f); inventoryHeldItem.SetActive(false);  return;

            case "Demon": hitCollider.GetComponentInParent<Demon>().DamageRecieved(); audioSource.PlayOneShot(ritualSFX, 1.3f); inventoryHeldItem.SetActive(false); break;
        }
    }

   
}
