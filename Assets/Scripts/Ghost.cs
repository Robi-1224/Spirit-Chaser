using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float timeToShoot;
    [SerializeField] int speed;
    [SerializeField] int rotationSpeed;
    [SerializeField] int dashForce;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] attackPattern;
    [SerializeField] AudioClip attackSFX;

    public List<GameObject> projInstance = new List<GameObject>();

    private Animator animator;
    private GameObject playerRef;
    private Rigidbody rb;
    private AudioSource source;

    private Vector3 targetPos;

    private bool canMove = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        source = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();

        if(projectile != null)
        {
            StartCoroutine(ProjectileAttack());
        }
        else
        {
            StartCoroutine(MeleeAttack());
        }

    }

    // Update is called once per frame
    void Update()
    {
        GhostMovement();
    }

    private void GhostMovement()
    {
        //continuously stalks the player
        Vector3 move = Vector3.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        targetPos = playerRef.transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(targetPos, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);

        if (canMove)
        transform.position = move;    
    }

    private IEnumerator ProjectileAttack()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(timeToShoot);
            yield return wait;
            source.PlayOneShot(attackSFX, 1);
            canMove = false;
            for(int i = 0; i < attackPattern.Length; i++)
            {
                var inst = Instantiate(projectile, attackPattern[i].position,Quaternion.identity);
                inst.transform.rotation = attackPattern[i].transform.rotation;
                projInstance.Add(inst);
            }
            yield return new WaitForSeconds(.3f);
            canMove = true;          
        }
    }
    
    private IEnumerator MeleeAttack()
    {
        // yielding bool prevents the loop to play multiple times/ stack, helps make the dash timing consistent
        bool yielding;
        while (true)
        {
            yielding = true;

            if (yielding)
            {
                WaitForSeconds wait = new WaitForSeconds(timeToShoot);
                yield return wait;
                animator.SetTrigger("melee");
                yield return new WaitForSeconds(1.15f);
                source.PlayOneShot(attackSFX, .8f);
                rb.AddForce(targetPos * dashForce, ForceMode.Impulse);
                yielding = false;
            }
        }
    }

}
