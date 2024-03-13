using UnityEngine;
using UnityEngine.InputSystem;

public class PickGrenade : MonoBehaviour
{
    
    public ThrowGrenade grenade;
 
    
    private void Awake()
    {
        

    }
  
    private void Update()
    {

    }

   
    
    public void OnTriggerEnter(Collider other)
    {
       
    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided"+other.gameObject.tag);
        if (other.gameObject.tag == "Grenade")
        {
            grenade.IncGrenade();
            Destroy(other.gameObject);
        }
    }

}
