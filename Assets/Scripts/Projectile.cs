
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
   
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            gameObject.SetActive(false);
            Destroy(gameObject,.3f);
        }
    }
}
