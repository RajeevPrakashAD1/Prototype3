using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public GameObject slot1;
    public GameObject slot2;
    private float speedIndex = 1.5f;
   
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.running == false) animator.SetBool("Running", false);
        if (GameManager.Instance.running == true) animator.SetBool("Running", true);
        if (GameManager.Instance.shooting == true) animator.SetBool("Shooting", true);
        if (GameManager.Instance.shooting == false) animator.SetBool("Shooting", false);
       // Debug.Log("active slot " + GameManager.Instance.ActiveSlot);
        if(GameManager.Instance.ActiveSlot == 1)
        {
            if(slot1.transform.childCount > 0)
            {
                GameManager.Instance.isHoldingGun = true;
               // GameManager.Instance.SpeedDecrease(speedIndex);
                animator.SetBool("HoldingGun", true);
            }
            else
            {
                GameManager.Instance.isHoldingGun = false;
               // GameManager.Instance.SpeedIncrease(speedIndex);
                animator.SetBool("HoldingGun", false);
            }
        }
        if (GameManager.Instance.ActiveSlot == 2)
        {
            if (slot2.transform.childCount > 0)
            {
                GameManager.Instance.isHoldingGun = true;
                //GameManager.Instance.SpeedDecrease(speedIndex);
                animator.SetBool("HoldingGun", true);
            }
            else
            {
                GameManager.Instance.isHoldingGun = false;
                //GameManager.Instance.SpeedIncrease(speedIndex);
                animator.SetBool("HoldingGun", false);
            }
        }
    }
}
