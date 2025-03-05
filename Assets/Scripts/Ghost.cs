using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int speed;

    private GameObject playerRef;
    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
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
        WaitForSeconds wait = new WaitForSeconds(3);


        yield return wait;
    }
}
