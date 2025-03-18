using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int speed;
    [SerializeField] float timeToShoot;
    [SerializeField] int rotationSpeed;
    [SerializeField] Transform[] attackPattern;
   
    private Animator animator;
    private GameObject playerRef;
    private bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

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
        var move = Vector3.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);

        if (canMove)
        transform.position = move;

        if (move != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-playerRef.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
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
            //make this until the animation is done playing
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
            new WaitForSeconds(1);
            // lanch the ghost forward
        }
    }

}
