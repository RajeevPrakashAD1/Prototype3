using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Player health
    
    public int playerHealth = 9900;
    public HealthBar healthBar;
    public GameObject player;
    public GameObject BigEnemy;
    public GameObject InstPrompt;
    public LevelData levelData;
    private float playerSpeed = 15f;
    public int currentLevel = 1;
    
    // Current weapon
    public GameObject currentWeapon { get; set; }
    //collided weapon
    public GameObject collidedWeapon { get; set; }
    public Transform gunPos;
    PlayerInput playerInput;
    // Public property to access the singleton instance
    public int numofEnemyKill = 0;
    public GameObject prompt;
    private InstPrompt promptScript;
    public int ActiveSlot =0;
    private bool bigEnemyPromptDisplayed;
    public bool gamePaused;
    public GameObject EnemyKillText;
    public GameObject BigEnemyParent;

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
   /* public IEnumerator Start()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2f);
        playerInput.enabled = false;
        playerInput.enabled = true;
        Time.timeScale = 1;
    }*/
    public void Awake()
    {
     
        Debug.Log("calling gameManagerAwake" + healthBar + player);
        // Ensure there's only one instance of the GameManager
        //Debug.Log("calling awake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //levelData = GameManager.instance.levelData;
        Application.targetFrameRate = 60;
        BigEnemyParent = GameObject.FindGameObjectWithTag("BigEnemyParent");
        BigEnemyParent.SetActive(false);
        EnemyKillText = GameObject.FindGameObjectWithTag("EnemyKillText");
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        playerInput = GetComponent<PlayerInput>();
        promptScript = prompt.GetComponentInChildren<InstPrompt>();
        promptScript.Invoke("Kill " + levelData.numOfEnemiesToKill.ToString() + " Enemy to Start Raid Battle");
        Debug.Log("calling awake of game manager");
    
        PauseGameForSeconds(3f);

    }
   
    public void Reset()
    {
        Debug.Log("calling reset of gamemanager");
        player = GameObject.FindGameObjectWithTag("Player");
       /* if(prompt == null)
        {
            prompt = GameObject.FindGameObjectWithTag("Prompt");
        }*/
     

        InventoryInitializer ii = GameObject.FindGameObjectWithTag("InventoryItemDisplay").GetComponent<InventoryInitializer>();
       /* ii.InitializeInventories();*/


       
        GameObject healthBarObject = GameObject.FindGameObjectWithTag("HealthBar");
        if(healthBarObject != null)
        {
            //Debug.Log("got health bar");
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }

        if (BigEnemyParent == null)
        {
            BigEnemyParent = GameObject.FindGameObjectWithTag("BigEnemyParent");
            BigEnemyParent.SetActive(false);
        }
        EnemyKillText = GameObject.FindGameObjectWithTag("EnemyKillText");
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        playerInput = GetComponent<PlayerInput>();
        promptScript = prompt.GetComponentInChildren<InstPrompt>();
        promptScript.Invoke("Kill " + levelData.numOfEnemiesToKill.ToString() + " Enemy to Start Raid Battle");
        PauseGameForSeconds(3f);
        collidedWeapon = null;
        currentWeapon = null;

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
        
        numofEnemyKill += 1;
        EnemyKillText.GetComponent<Text>().text = "EnemyKill:" + numofEnemyKill.ToString();
        Debug.Log("callling kill enemy" + numofEnemyKill);
        if (numofEnemyKill >= levelData.numOfEnemiesToKill && !bigEnemyPromptDisplayed)
        {
            //will show prompt of big enemy appearance.p
            BigEnemyParent.SetActive(true);
            prompt.SetActive(true);
            
             
            string txt = "Kill " + levelData.numberOfBigEnemies.ToString() + " BigEnemy to  complete this level";
            promptScript.Invoke(txt);
            PauseGameForSeconds(3f);
            
            bigEnemyPromptDisplayed = true;

        }
    }

    public void ChangeActiveSlot(int i)
    {
        ActiveSlot = i;

        player.GetComponent<PickGun>().Pick();
    }
    void PauseGameForSeconds(float duration)
    {
        
        Time.timeScale = 0f;
        StartCoroutine(ResumeAfterDelay(duration));
    }

    IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 1f;
        
    }
    
}
