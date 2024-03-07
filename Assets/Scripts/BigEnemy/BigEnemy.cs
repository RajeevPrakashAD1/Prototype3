using UnityEngine;
using UnityEngine.UI;

public class BigEnemy : MonoBehaviour
{
    public int maxHits = 15; // Maximum number of hits allowed before destruction
    private int currentHits = 0; // Current number of hits
    public Slider slider;
    public LevelProgression levelProg;
    public void Start()
    {
        slider.maxValue = 1000f;
        slider.value = 1000f;
    }

    public void Update()
    {
        if(slider.value <= 0)
        {
            Destroy(gameObject);
            levelProg.KillBigEnemy();
            
        }
    }
    public void SetHealth(int bulletDamage)
    {
        //Debug.Log("setting health");
        slider.value -= bulletDamage;

    }
    /*private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a player bullet
        Debug.Log("getting big enemy collided"+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("calling collisin");   
            SetHealth(200);   
        }
    }*/
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the collision is with a player bullet
        //Debug.Log("getting big enemy collided" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("calling collisin");
            SetHealth(40);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameManager.Instance.DamagePlayer(10000);
        }
    }
}
