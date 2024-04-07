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
    public int playerHealth = 9999;
    public HealthBar healthBar;
    public GameObject player;
    public GameObject BigEnemy;
    public GameObject InstPrompt;
    public LevelData levelData;
    private float playerSpeed = 12f;
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
    public int ActiveSlot = 1;
    private bool bigEnemyPromptDisplayed;
    public bool gamePaused;
    public GameObject EnemyKillText;
    public GameObject timerText;

    public float elapsedTime = 0f;
    public GameObject BigEnemyParent;
    public bool running;
    public bool shooting;
    public bool isHoldingGun;
    public GameObject nextLevelButton;
    private GameObject parentNextLevelButton;
    public int numOfBigEnemy;
    public GameObject healthBarObject;
    public InventoryInitializer ii;
    public bool isProtectionOn = false;
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
    public void Update()
    {
    
        elapsedTime += Time.deltaTime;

        // Calculate minutes and seconds from the elapsed time
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        
        // Format the timer text as minutes:seconds
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update the UI text to display the timer
       // if (timerText != null)
        //{
            timerText.GetComponent<Text>().text = timerString;
        //}
        //Debug.Log("player speed" + playerSpeed);
    }
    public void Awake()
    {
        //Debug.Log("calling awake of game manager");
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
        timerText = GameObject.FindGameObjectWithTag("Timer");
        nextLevelButton = GameObject.FindGameObjectWithTag("NextLevelButton");
        nextLevelButton.SetActive(false);
        parentNextLevelButton = GameObject.FindGameObjectWithTag("ParentNextLevelButton");
        playerInput = GetComponent<PlayerInput>();


        prompt = GameObject.FindGameObjectWithTag("Prompt");
        promptScript = prompt.GetComponentInChildren<InstPrompt>();

        //Debug.Log("prompt script" + promptScript);
        promptScript.Invoke("Kill " + levelData.numOfEnemiesToKill.ToString() + " Enemy to Start Raid Battle");
        numOfBigEnemy = levelData.numberOfBigEnemies;



        PauseGameForSeconds(3f);
        running = false;
        shooting = false;
        isHoldingGun = false;
        isProtectionOn = false;

    }
   
    public void Reset()
    {
        Debug.Log("calling reset of GameManager");
        if(player == null) player = GameObject.FindGameObjectWithTag("Player");

        // Ensure that the player GameObject is not null before further operations
        if (player == null)
        {
            Debug.LogError("Player GameObject not found.");
        }
        else
        {
            // If the player GameObject is found, proceed with further operations


            // Search for InventoryInitializer only if it is null
            if (ii == null)
            {
                ii = GameObject.FindGameObjectWithTag("InventoryItemDisplay")?.GetComponent<InventoryInitializer>();
                if (ii == null)
                {
                    Debug.LogError("InventoryInitializer component not found.");
                }
            }

            // Search for HealthBar GameObject only if it is null
            if (healthBarObject == null)
            {
                healthBarObject = GameObject.FindGameObjectWithTag("HealthBar");
                if (healthBarObject == null)
                {
                    Debug.LogError("HealthBar GameObject not found.");
                }
                else
                {
                    healthBar = healthBarObject.GetComponent<HealthBar>();
                }
            }

            // Search for BigEnemyParent GameObjects only if the array is null
            if (BigEnemyParent == null)
            {
                BigEnemyParent = GameObject.FindGameObjectWithTag("BigEnemyParent");
                if (BigEnemyParent == null)
                {
                    Debug.LogError("BigEnemyParent GameObject not found.");
                }
                else {
                    if (BigEnemyParent.activeSelf)
                    {
                        BigEnemyParent.SetActive(false);
                    }
                }
            }

            // Search for EnemyKillText GameObject only if it is null
            if (EnemyKillText == null)
            {
                EnemyKillText = GameObject.FindGameObjectWithTag("EnemyKillText");
                if (EnemyKillText == null)
                {
                    Debug.LogError("EnemyKillText GameObject not found.");
                }
            }

            // Search for Timer GameObject only if it is null
            if (timerText == null)
            {
                timerText = GameObject.FindGameObjectWithTag("Timer");
                if (timerText == null)
                {
                    Debug.LogError("Timer GameObject not found.");
                }
            }

            // Search for Prompt GameObject only if it is null
            if (prompt == null) prompt = GameObject.FindGameObjectWithTag("Prompt");
            
               
            if (prompt == null)
            {
                Debug.LogError("Prompt GameObject not found.");
            }
            else
            {
                // If prompt is found, retrieve its InstPrompt component
                promptScript = prompt.GetComponentInChildren<InstPrompt>();
                if (promptScript == null)
                {
                    Debug.LogError("InstPrompt component not found in Prompt GameObject.");
                }
                else
                {
                    // Invoke the prompt
                    prompt.SetActive(true);
                    promptScript.Invoke("Kill " + levelData.numOfEnemiesToKill.ToString() + " Enemy to Start Raid Battle");
                }
            }
            

            // Search for NextLevelButton GameObject only if it is null
            if (nextLevelButton == null)
            {
                nextLevelButton = GameObject.FindGameObjectWithTag("NextLevelButton");
                if (nextLevelButton == null)
                {
                    Debug.LogError("NextLevelButton GameObject not found.");
                }
                else
                {
                    nextLevelButton.SetActive(false);
                }
            }

            // Search for ParentNextLevelButton GameObject only if it is null
            if (parentNextLevelButton == null)
            {
                parentNextLevelButton = GameObject.FindGameObjectWithTag("ParentNextLevelButton");
                if (parentNextLevelButton == null)
                {
                    Debug.LogError("ParentNextLevelButton GameObject not found.");
                }
            }

            // Pause the game for 3 seconds
            PauseGameForSeconds(3f);

            // Reset other variables and flags
            collidedWeapon = null;
            currentWeapon = null;
            playerHealth = 9999;
            playerSpeed = 12f;
            numofEnemyKill = 0;
            ActiveSlot = 1;
            bigEnemyPromptDisplayed = false;
            running = false;
            shooting = false;
            isHoldingGun = false;
            numOfBigEnemy = levelData.numberOfBigEnemies;
            elapsedTime = 0f;
            isProtectionOn = false;

        }



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
        if (isProtectionOn == false)
        {
            playerHealth -= damageAmount;
            //Debug.Log("player health " + playerHealth);
            healthBar.SetHealth(playerHealth);

            // Check if player health is zero or below
            if (playerHealth <= 0)
            {

                ShowNextLevelButton();
                playerHealth = 10000000;
                Time.timeScale = 0f;

            }
        }
    }
    public void HealthIncrease(int IncreaseAmount)
    {
        playerHealth += IncreaseAmount;
        healthBar.SetHealth(playerHealth);

        
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
        //Debug.Log("callling kill enemy" + numofEnemyKill);
        if (numofEnemyKill >= levelData.numOfEnemiesToKill && !bigEnemyPromptDisplayed)
        {
            //will show prompt of big enemy appearance.p
            BigEnemyParent.SetActive(true);
            prompt.SetActive(true);
            
             
            string txt = "Kill " + levelData.numberOfBigEnemies.ToString() + " BigEnemy to  complete this level";
           

            if(promptScript != null) promptScript.Invoke(txt);
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

    public void ShowNextLevelButton()
    {


       
        nextLevelButton.SetActive(true);
        parentNextLevelButton.GetComponent<DataManipulation>().UpdateText();
        DataPersistentManager.Instance.SaveGame();
        //Time.timeScale = 0f;
    }

}
