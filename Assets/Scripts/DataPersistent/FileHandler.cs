using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private string dataDirPath = "";
    private string dataFileName = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FileHandler(string name, string path)
    {
        dataDirPath = path;
        dataFileName = name;
    }

    public GameData Load()
    {
        Debug.Log(dataDirPath + " " + dataFileName);
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        Debug.Log("load full path " + fullPath);
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file

                /* string dataToLoad = "";
                 using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                 {
                     using (StreamReader reader = new StreamReader(stream))
                     {
                         dataToLoad = reader.ReadToEnd();
                     }
                 }*/
                string dataToLoad = File.ReadAllText(fullPath);



                // optionally decrypt the data


                // deserialize the data from Json back into the C# object
                Debug.Log("data..." + dataToLoad);
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }catch(Exception e)
            {
                Debug.LogError("load gamedata error" + e);
            }

        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //Debug.Log("fullpath" + fullPath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            /*using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);

                }
            }*/
            File.WriteAllText(fullPath, dataToStore);
        }
        catch(Exception e)
        {
            Debug.Log("file handling error" + e);
        }
    }
}
