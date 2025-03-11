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
    [SerializeField] int rotationSpeed;

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
        // uses the new input system to read the value of the "Movement" tab in the settings, then moves the player to the assigned direction depending on the button pressed using the speed variable
        // rotation made with help by this video: https://www.youtube.com/watch?v=BJzYGsMcy8Q
        Vector2 dir = MovementInput.ReadValue<Vector2>();
        var movementDirection = new Vector3(dir.x, 0, dir.y) * speed * Time.deltaTime;
        transform.position += movementDirection;
        if (movementDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Dash()
    {
        // looks if the last attack has been longer ago than the cooldown, if it is then the player dashes towards movement direction
        // the if statement is from a stackOverflow comment
        if (Time.time - lastAttack< attackCooldown) return;
        Vector2 dir = MovementInput.ReadValue<Vector2>();
        Vector3 movement = new Vector3(dir.x, 0, dir.y);
        rb.AddForce(movement * dashForce, ForceMode.Impulse);
        lastAttack = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }

}
