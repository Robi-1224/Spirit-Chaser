using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : RitualObject
{
    [SerializeField] GameObject heldObject;


    PlayerInput playerInput;
    InputAction interaction;

    RaycastHit hit;

    private void Awake()
    {
        playerInput= GetComponent<PlayerInput>();
        interaction = playerInput.actions.FindAction("Interaction");
    }

    public void interact()
    {
        Physics.Raycast(transform.position,Vector3.forward,out hit,2.5f);
        
        if (hit.collider.gameObject.CompareTag("Held item"))
        {
            heldObject = hit.collider.gameObject;
        }
        else if (hit.collider.gameObject.CompareTag("No held item ritual"))
        {
            ItemBehaviour();
        }
        else if (hit.collider.gameObject.CompareTag("Held item ritual") && heldObject != null)
        {
            ItemBehaviour();
        }
        else
        {
            Debug.Log("Need the held item");
        }
    }
    

}
