using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Player health
    private int playerHealth = 1300;
    public HealthBar healthBar;
    

    // Current weapon
    private string currentWeapon = "Pistol";

    // Public property to access the singleton instance
    public static GameManager Instance
    {
        get
        {
            // If the instance doesn't exist yet, find it in the scene or create a new one
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                }
            }

            return instance;
        }
    }

    // Public property to access player health
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    // Public property to access current weapon
    public string CurrentWeapon
    {
        get { return currentWeapon; }
        set { currentWeapon = value; }
    }

    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Example method to damage the player
    public void DamagePlayer(int damageAmount)
    {
        playerHealth -= damageAmount;
        healthBar.SetHealth(playerHealth);

        // Check if player health is zero or below
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
            // Handle player death
        }
    }

    // Example method to switch the current weapon
    public void SwitchWeapon(string newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Player switched to " + newWeapon);
    }
}
