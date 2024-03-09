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
        Vector3 direction = (target - transform.position).normalized;
        Vector3 newTarget = target + direction * 0.3f;
        transform.position = Vector3.MoveTowards(transform.position, newTarget, speed * Time.deltaTime);
        Debug.Log(transform.position + " " + target);
        if (Vector3.Distance(transform.position, newTarget) <= 0.1f)
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
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with rigid: " + collision.gameObject.tag, collision.gameObject);


        // Destroy the bullet and the collided object
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }

}
    