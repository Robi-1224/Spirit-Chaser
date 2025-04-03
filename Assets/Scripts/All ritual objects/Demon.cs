using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Projectile
{
    private Ghost ghostRef;
    private PlayerInteraction playerInteractionRef;

    private List<GameObject> projectiles = new List<GameObject>();
    private GameObject heldObjectBase;

    [SerializeField] int health;
    [SerializeField] float waitToRandom;
    private int maxSpeed = 6;
    private int minSpeed = 3;
    private void Awake()
    {
        ghostRef = GetComponent<Ghost>();
        playerInteractionRef = FindAnyObjectByType<PlayerInteraction>();
        heldObjectBase = playerInteractionRef.heldObject;
        projectiles = ghostRef.projInstance;

        StartCoroutine(SpeedRandomizerProjectiles());
    }
    private void Update()
    {
        HealthController();
    }
    private void HealthController()
    {
        switch(health)
        {
            case 0: Debug.Log("you won"); return;

            case 1: minSpeed = 5; maxSpeed = 8; return;

            case 2: minSpeed = 4; maxSpeed = 7; return;
        }
      
    }

    // Gets the current amount of projectiles shot, and randomizes them with the min and max speed variable.
    private IEnumerator SpeedRandomizerProjectiles()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(waitToRandom);
            yield return wait;
            for(int i=0; i<projectiles.Count; i++)
            {
                projectiles[i].GetComponent<Projectile>().speed = Random.Range(minSpeed, maxSpeed);
            }
            projectiles.Clear();
        }
    }

    public void DamageRecieved()
    {
        health--;
        Destroy(playerInteractionRef.heldObject);
        playerInteractionRef.heldObject = heldObjectBase;
        //play damage sound
    }
}
