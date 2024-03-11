using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;
    public BulletData bulletData;
   
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    public float damage { get; set; }
    private void OnEnable()
    {
        //Destroy(gameObject, bulletData.ttl);
    }
    private void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        float distanceAhead = 0.5f; // Adjust this value to determine how far ahead the new target should be
        Vector3 newTarget = target + direction * distanceAhead;
        transform.position = Vector3.MoveTowards(transform.position, target, bulletData.speed * Time.deltaTime);
         Debug.Log(transform.position + " " + target);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            //Destroy(gameObject);
            Debug.Log("reached pos");
        }

    }
    private void Start()
    {
       // Debug.Log("target + " + target);


    }
     
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag , collision.gameObject);


        // Destroy the bullet and the collided object
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }

    }
   

}
    