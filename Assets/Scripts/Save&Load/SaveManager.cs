using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{  
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private string filePath = "idbfs/mehmetyildiz20041991";
    [SerializeField] private bool encryptData;
    
    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete Save File")]
    public void DeleteSavedData()
    {
        //dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData); // Default kayýr yeri olursa bunu kullan.
        dataHandler = new FileDataHandler(filePath, fileName, encryptData); // WebGL olursa bunu.
        dataHandler.DeleteData();
    }


    private void Awake()
    {
        if(instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;           

        }


        //dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData); // Default kayýr yeri olursa bunu kullan.
        dataHandler = new FileDataHandler(filePath, fileName, encryptData); // WebGL olursa bunu.
        saveManagers = FindAllSaveManagers();
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {            
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSavedData()
    {
        GameData loadedData = dataHandler.Load();
        if (loadedData != null )
        {
            return true;
        }
        return false;
    }
}
