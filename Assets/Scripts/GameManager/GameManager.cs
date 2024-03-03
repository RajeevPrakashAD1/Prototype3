using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Player health
    private int playerHealth = 9900;
    public HealthBar healthBar;
    public GameObject player;
    public LevelData levelData;
    private float playerSpeed = 12f;
    

    // Current weapon
    public GameObject currentWeapon;
    //collided weapon
    public GameObject collidedWeapon;

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
    private void Awake()
    {
        //Debug.Log("calling gameManagerAwake" + healthBar + player);
        // Ensure there's only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        //levelData = GameManager.instance.levelData;
        
    }
   
    public void Reset()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            //Debug.Log("didnt get playere");
        }
        GameObject healthBarObject = GameObject.FindGameObjectWithTag("HealthBar");
        if(healthBarObject != null)
        {
            //Debug.Log("got health bar");
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }
        else
        {
            Debug.Log("didnt get healthBar");
        }
        playerHealth = 9900;
        playerSpeed = 12f;

    }

    public void SetLevelData(LevelData data)
    {
        levelData = data;
    }
    // Public property to access player health
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }
    // Public property to access current weapon
    public GameObject CurrentWeapon
    {
        get { return currentWeapon; }
        set { currentWeapon = value; }
    }
    public GameObject CollidedWeapon
    {
        get { return collidedWeapon; }
        set { collidedWeapon = value; }
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
    public void HealthIncrease(int IncreaseAmount)
    {
        playerHealth += IncreaseAmount;
        healthBar.SetHealth(playerHealth);

        // Check if player health is zero or below
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
            // Handle player death
        }
    }

    public void SpeedIncrease(float amount)
    {
        PlayerSpeed *= amount;
    }
    public void SpeedDecrease(float amount)
    {
        PlayerSpeed /= amount;
    }
    // Example method to switch the current weapon
    public void SwitchWeapon(GameObject newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Player switched to ");
    }
}
