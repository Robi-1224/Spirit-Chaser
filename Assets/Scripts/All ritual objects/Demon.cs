using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    private Ghost ghostRef;
    private RoomManager roomManagerRef;
    private PlayerInteraction playerInteractionRef;

    [SerializeField] Transform healthBar;
    private GameObject heldObjectBaseRef;

    private List<GameObject> projectiles = new List<GameObject>();

    [SerializeField] int health;
    [SerializeField] float waitToRandom;

    private int maxSpeed = 6;
    private int minSpeed = 3;
    private Vector2 healthBarSize;

    private void Awake()
    {
        ghostRef = GetComponent<Ghost>();
        playerInteractionRef = FindAnyObjectByType<PlayerInteraction>();
        roomManagerRef = FindAnyObjectByType<RoomManager>();

        projectiles = ghostRef.projInstance;
        heldObjectBaseRef = playerInteractionRef.heldObject;
        healthBarSize = healthBar.localScale;
       
        StartCoroutine(SpeedRandomizerProjectiles());
    }
    private void Update()
    {
        HealthController();
    }
    private void HealthController()
    {
        healthBar.localScale = healthBarSize;
        switch (health)
        {
            case 0:  return;

            case 1: minSpeed = 5; maxSpeed = 8;  return;

            case 2: minSpeed = 4; maxSpeed = 7; return;

        }     
    }

    // Gets the current amount of projectiles shot, and randomizes them with the min and max speed variable.
    private IEnumerator SpeedRandomizerProjectiles()
    {
        while (true)
        {         
            WaitForSeconds wait = new WaitForSeconds(waitToRandom);
            projectiles.Clear();
            yield return wait;
            for(int i=0; i<projectiles.Count; i++)
            {
                projectiles[i].GetComponent<Projectile>().speed = Random.Range(minSpeed, maxSpeed);
            }        
        }
    }

    public void DamageRecieved()
    {
        healthBarSize.x = healthBarSize.x - health;
        health--;
        roomManagerRef.ritualList.Remove(playerInteractionRef.heldObject);
        Destroy(playerInteractionRef.heldObject);
        playerInteractionRef.heldObject = heldObjectBaseRef;
        //play damage sound
    }
}
