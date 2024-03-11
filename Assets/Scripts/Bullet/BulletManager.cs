using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // this scipt handle which bullet is choosen for shooting;
    public GameObject bullet;
    public int BulletHoldingid;
    public BulletInventory bulletInventory;
    public void SetBullet(GameObject _bullet,int id)
    {
        
        bullet = _bullet;
        BulletHoldingid = id;
       // Debug.Log("setting bullet"+bullet);

    }
}
