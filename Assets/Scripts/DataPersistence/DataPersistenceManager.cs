using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one data Persistence MAnager in the scene");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Application.persistentDataPath);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - Avaa mikä tahansa data data handlerista
        this.gameData = dataHandler.Load();
        
        //Jos ei löydy sitten kysy uutta peliä
        if (this.gameData == null)
        {
            Debug.Log("Data not found");
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
