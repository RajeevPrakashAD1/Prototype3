using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPowerUp : MonoBehaviour
{
    
    public GameObject protectionSphere;
    // Function to start the protection sphere for 5 seconds
    void Start()
    {
        // Assuming the protection sphere is a child of the player GameObject

        
    }
    public void StartProtectionSphere(float duration)
    {
       
        
        StartCoroutine(ActivateProtectionSphere(duration));
        
    }

    // Coroutine to activate the protection sphere for a specified duration
    private IEnumerator ActivateProtectionSphere(float duration)
    {
        
        // Activate the protection sphere
        ActivateProtection();
        yield return new WaitForSeconds(duration);
        // Deactivate the protection sphere after the duration
        DeactivateProtection();
        
    }

    // Function to increase the player's speed for 5 seconds
    public void IncreasePlayerSpeed(float speedMultiplier, float duration)
    {
        
            StartCoroutine(IncreaseSpeedForDuration(speedMultiplier, duration));
        
    }

    // Coroutine to increase the player's speed for a specified duration
    private IEnumerator IncreaseSpeedForDuration(float speedMultiplier, float duration)
    {
       
        // Increase the player's speed
        IncreaseSpeed(speedMultiplier);
        yield return new WaitForSeconds(duration);
        // Reset the player's speed after the duration
        ResetSpeed(speedMultiplier);
        
    }

    // Function to increase the player's health
    public void IncreasePlayerHealth(int healthIncrease)
    {
        // Increase the player's health logic goes here
        GameManager.Instance.HealthIncrease(healthIncrease);
        Debug.Log("Player health increased by " + healthIncrease);
    }

    // Function to activate the protection sphere
    private void ActivateProtection()
    {
        // Activate the protection sphere logic goes here
        protectionSphere.SetActive(true);
        GameManager.Instance.isProtectionOn = true;
        Debug.Log("Protection sphere activated");
    }

    // Function to deactivate the protection sphere
    private void DeactivateProtection()
    {
        // Deactivate the protection sphere logic goes here
        protectionSphere.SetActive(false);
        GameManager.Instance.isProtectionOn = false;
        Debug.Log("Protection sphere deactivated");
    }

    // Function to increase the player's speed
    private void IncreaseSpeed(float speedMultiplier)
    {
        // Increase the player's speed logic goes here
        GameManager.Instance.SpeedIncrease(speedMultiplier);
        Debug.Log("new speed" + GameManager.Instance.PlayerSpeed);
    }

    // Function to reset the player's speed
    private void ResetSpeed(float sm)
    {
        // Reset the player's speed logic goes here
        
        GameManager.Instance.SpeedDecrease(sm);
        Debug.Log("Player speed reset");
    }
}
