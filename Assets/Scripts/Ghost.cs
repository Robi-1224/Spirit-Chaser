using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int speed;
    [SerializeField] float timeToShoot;
    [SerializeField] Transform[] attackPattern;
   

    private GameObject playerRef;
    private bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

        if(projectile != null)
        {
            StartCoroutine(ProjectileAttack());
        }
        else
        {
            //melee attack ienumerator
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
        if (canMove)
        transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
    
        
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

}
