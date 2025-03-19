using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
  
    [SerializeField] int speed;
    [SerializeField] float timeToShoot;
    [SerializeField] int rotationSpeed;
    [SerializeField] int dashForce;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] attackPattern;
   
    private Animator animator;
    private GameObject playerRef;
    private Rigidbody rb;

    private bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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
       

        if (canMove)
        transform.position = move;

        if (move != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-playerRef.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ProjectileAttack()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(timeToShoot);
            yield return wait;
            canMove = false;
            for(int i = 0; i < attackPattern.Length; i++)
            {
                var inst = Instantiate(projectile, attackPattern[i].position,Quaternion.identity);
                inst.transform.rotation = attackPattern[i].transform.rotation;
            }
            yield return new WaitForSeconds(.3f);
            canMove = true;          
        }
    }
    
    private IEnumerator MeleeAttack()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(timeToShoot);
            yield return wait;

            animator.SetTrigger("melee");
            yield return new WaitForSeconds(1.3f);
         
          //  rb.AddForce(-playerRef.transform.position * 1, ForceMode.Impulse);
        }
    }

}
