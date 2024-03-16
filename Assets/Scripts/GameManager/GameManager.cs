using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Player health
    
    public int playerHealth = 9900;
    public HealthBar healthBar;
    public GameObject player;
    public GameObject InstPrompt;
    public LevelData levelData;
    private float playerSpeed = 15f;
    public int currentLevel = 1;

    // Current weapon
    public GameObject currentWeapon;
    //collided weapon
    public GameObject collidedWeapon;
    public Transform gunPos;
    PlayerInput playerInput;
    // Public property to access the singleton instance
    public int numofEnemyKill = 0;
    public GameObject prompt;
    private bool bigEnemyPromptDisplayed;
    public int ActiveSlot =0;

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
        Application.targetFrameRate = 60;
        playerInput = GetComponent<PlayerInput>();

    }
   
    public void Reset()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       /* if(prompt == null)
        {
            prompt = GameObject.FindGameObjectWithTag("Prompt");
        }*/
        playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = false;
        playerInput.enabled = true;

        InventoryInitializer ii = GameObject.FindGameObjectWithTag("InventoryItemDisplay").GetComponent<InventoryInitializer>();
       /* ii.InitializeInventories();*/


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
        playerSpeed = 15f;
        numofEnemyKill = 0;
        ActiveSlot = 0;


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

    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
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

    public void KillEnemy()
    {
        numofEnemyKill++;
        if(numofEnemyKill >= levelData.numOfEnemiesToKill && !bigEnemyPromptDisplayed) { }
        {
            //will show prompt of big enemy appearance.
            TextMeshProUGUI tmp = prompt.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = "Go Kill All The Big Enemy To Complete this Level";
            bigEnemyPromptDisplayed = true;

        }
    }

    public void ChangeActiveSlot(int i)
    {
        ActiveSlot = i;

        player.GetComponent<PickGun>().Pick();
    }
}
