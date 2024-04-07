using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataPersistentManager : MonoBehaviour
{
    [Header("File Storage Config")]
    private string fileName = "gamedata";
    private FileHandler dataHandler;
    private static DataPersistentManager instance;
    private GameData gameData;
    private List<IDataPersistence> dataPersistentObjects;

    public static DataPersistentManager Instance
    {
        get
        {
            // If the instance doesn't exist yet, find it in the scene or create a new one
            if (instance == null)
            {
                instance = FindObjectOfType<DataPersistentManager>();

                if (instance == null)
                {
                    GameObject singletonObject2 = new GameObject();
                    instance = singletonObject2.AddComponent<DataPersistentManager>();
                    singletonObject2.name = typeof(DataPersistentManager).ToString() + " (Singleton)";
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
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

    }
    private void Start()
    {
        this.dataPersistentObjects = FindAllDataPersistentObjects();

       // Debug.Log(dataPersistentObjects.Count);
        this.dataHandler = new FileHandler(fileName,Application.persistentDataPath);
        LoadGame();
    }
    public void Reset()
    {
        Debug.Log("calling reset data persiste");
        this.dataPersistentObjects = FindAllDataPersistentObjects();

        // Debug.Log(dataPersistentObjects.Count);
        this.dataHandler = new FileHandler(fileName, Application.persistentDataPath);
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();

    }
    public void LoadGame()
    {
        //load game
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();

        }
        foreach (IDataPersistence dataPerObj in dataPersistentObjects)
        {
            //Debug.Log("datapreobj" + dataPerObj);
            dataPerObj.LoadData(gameData);
        }

       // Debug.Log("loaded death count" + gameData.KillCount);

    }
    public void SaveGame()
    {
        //save game data
       
        foreach (IDataPersistence dataPerObj in dataPersistentObjects)
        {
            dataPerObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
        Debug.Log("save death count" + gameData.coins);
    }

    private List<IDataPersistence> FindAllDataPersistentObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}
