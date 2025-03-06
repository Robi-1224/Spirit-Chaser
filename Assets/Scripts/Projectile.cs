using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int speed;
   
    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
      
      if (other.tag != gameObject.tag)
         Destroy(gameObject);
    }
}
