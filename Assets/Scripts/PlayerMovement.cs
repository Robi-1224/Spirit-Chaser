using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction MovementInput;
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        MovementInput = playerInput.actions.FindAction("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 dir = MovementInput.ReadValue<Vector2>();
        transform.position += new Vector3(dir.x, 0,dir.y) * speed * Time.deltaTime;
    }
}
