
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction MovementInput;
    InputAction pauseInput;
    RoomManager roomManager;
    ButtonManager buttonManager;

    Rigidbody rb;

    [SerializeField] int speed;
    [SerializeField] int dashForce;
    [SerializeField] int rotationSpeed;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioClip deathSFX;

    private float lastAttack;
    private float attackCooldown = 1f;
    private Vector2 moveDir = Vector2.zero;
    private AudioSource walkingSource;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        MovementInput = playerInput.actions.FindAction("Movement");
        pauseInput = playerInput.actions.FindAction("Pause");
        roomManager = FindAnyObjectByType<RoomManager>();
        walkingSource= GetComponent<AudioSource>();
        buttonManager = FindAnyObjectByType<ButtonManager>();
        audioSource = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
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
        moveDir = MovementInput.ReadValue<Vector2>();
        var movementDirection = new Vector3(moveDir.x, 0, moveDir.y) * speed * Time.deltaTime;
        transform.position += movementDirection;
        if (movementDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            walkingSource.enabled= true;
        }
        else
        {
            walkingSource.enabled= false;
        }
    }

    public void PauseTheGame()
    {
        if (pauseInput.triggered)
        {
            buttonManager.PauseTheGame();
        }
    }
    public void Dash()
    {
        // looks if the last attack has been longer ago than the cooldown, if it is then the player dashes towards movement direction
        // the if statement is from a stackOverflow comment
        if (Time.time - lastAttack < attackCooldown) return;
        moveDir = MovementInput.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.y);
        rb.AddForce(movement * dashForce, ForceMode.Impulse);
        lastAttack = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Enemy"))
      {
        audioSource.PlayOneShot(deathSFX, 2);
        gameOverPanel.SetActive(true);
        Destroy(gameObject);

      }
      else if(other.gameObject.CompareTag("Clear room door"))
      {
         roomManager.LoadNextLevel();
      }
    }


}
