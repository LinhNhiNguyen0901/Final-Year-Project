//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class dataPersistenceManager : MonoBehaviour
//{
//    [Header("File Storage Config")]
//    [SerializeField] private string fileName;
//    [SerializeField] private bool useEncryption;

//    private gameData gameData;
//    private List<IdataPersistence> dataPersistenceObjects;
//    private FileDataHandler dataHandler;

//    public static dataPersistenceManager instance { get; private set; }

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Debug.LogError("There's more than one Data Persistence Manager in the scene.");
//        }
//        instance = this;
//    }

//    public void Start()
//    {
//        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
//        LoadGame();
//    }

//    public void NewGame()
//    {
//        this.gameData = new gameData();
//    }

//    public void LoadGame()
//    {
//        if (this.gameData == null)
//        {
//            Debug.Log("No data was found. Initializing data to defaults.");
//            NewGame();
//        }

//        // push the loaded data to all other scripts that need it
//        foreach (IdataPersistence dataPersistenceObj in dataPersistenceObjects)
//        {
//            dataPersistenceObj.LoadData(gameData);
//        }
//    }

//    public void SaveGame()
//    {
//        foreach (IdataPersistence dataPersistenceObj in dataPersistenceObjects)
//        {
//            dataPersistenceObj.SaveData(gameData);
//        }

//        // save that data to a file using the data handler
//        dataHandler.Save(gameData);
//    }

//    private void OnApplicationQuit()
//    {
//        SaveGame();
//    }

//    private List<IdataPersistence> FindAllDataPersistenceObjects()
//    {
//        IEnumerable<IdataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
//            .OfType<IdataPersistence>();

//        return new List<IdataPersistence>(dataPersistenceObjects);
//    }
//}
