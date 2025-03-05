using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction MovementInput;

    Rigidbody rb;

    [SerializeField] int speed;
    [SerializeField] int dashForce;

    private float lastAttack;
    private float attackCooldown = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        transform.position += new Vector3(dir.x, 0, dir.y) * speed * Time.deltaTime;
    }

    public void Dash()
    {
        if (Time.time - lastAttack< attackCooldown) return;
        Vector2 dir = MovementInput.ReadValue<Vector2>();
        Vector3 movement = new Vector3(dir.x, 0, dir.y);
        rb.AddForce(movement * dashForce, ForceMode.Impulse);
        lastAttack = Time.time;
    }

     

}
