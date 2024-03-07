using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;

    private float speed = 200f;
    private float timeToDestory = 100f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    public float damage { get; set; }
    private void OnEnable()
    {
        Destroy(gameObject, timeToDestory);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ( Vector3.Distance(transform.position, target) < 0.1f)
        {
           Destroy(gameObject);
        }
        //Debug.Log(transform.position + " " + target);
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
        else
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }

    }

}
    