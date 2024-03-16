using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;
    public BulletData bulletData;
    public float bulletForce = 1;
    private Transform mainCamera;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    public float damage { get; set; }
    private void OnEnable()
    {
       Destroy(gameObject, bulletData.ttl);
    }
    private void Update()
    {
        

    }
    private void Start()
    {
        mainCamera = Camera.main.transform;
      
       int layer1 = LayerMask.NameToLayer("Player");
       int layer2 = LayerMask.NameToLayer("PlayerProtection");
       int layer3 = LayerMask.NameToLayer("PlayerGun");
       int layer4 = LayerMask.NameToLayer("Enemy");
       int layer5 = LayerMask.NameToLayer("BigEnemy");

       // Combine layers using bitwise OR operations
       int excludedLayers = (1 << layer1) | (1 << layer2) | (1 << layer3)  ;



      /*  Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
           Debug.LogError("Rigidbody component not found on bullet prefab.");
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, mainCamera.forward, out hit, 300f,excludedLayers))
        {
            Debug.Log("hit : " + hit.collider.gameObject + " " + hit.point);
            Vector3 direction = hit.point - transform.position;
           if (rb != null)
           {
               rb.AddForce(direction.normalized * (bulletForce * bulletData.speed));
           }
        }
        else
        {
            Debug.Log("going in else");
           rb.AddForce(mainCamera.position * (bulletForce * bulletData.speed));
        }*/

    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.tag , collision.gameObject);


        // Destroy the bullet and the collided object
        if (collision.gameObject.tag == "Enemy")
        {
           Destroy(gameObject);
           Destroy(collision.gameObject);
            GameManager.Instance.KillEnemy();
        }
        if(collision.gameObject.tag == "Terrain")
        {
           Destroy(gameObject);
        }

    }


}
