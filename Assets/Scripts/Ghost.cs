using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int speed;
    [SerializeField] Transform[] attackPattern;

    private GameObject playerRef;
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
        transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position,speed * Time.deltaTime);
    }

    private IEnumerator ProjectileAttack()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(3);
            for(int i = 0; i < attackPattern.Length; i++)
            {
                Instantiate(projectile, attackPattern[i].position,Quaternion.identity);
            }

            yield return wait;
        }
    }

}
