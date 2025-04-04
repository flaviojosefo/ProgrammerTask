using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Save Settings")]
    [SerializeField] private string saveName = "save";
    [SerializeField] private string saveDirectory = "FlavioSantos_Task";

    // The path to the "My Games" folder, in Documents
    private readonly string _myGames =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");

    // Important save paths
    private string _saveFileWithExtension;
    private string _saveDirectory;
    private string _saveFile;

    // Singleton
    private static SaveManager _instance;
    public static SaveManager Instance => _instance;

    private void Awake()
    {
        // Setup Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        // Maintain through Scene loads
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Setup pertinent paths
        _saveFileWithExtension = $"{saveName}.json";
        _saveDirectory = Path.Combine(_myGames, saveDirectory);
        _saveFile = Path.Combine(_saveDirectory, _saveFileWithExtension);
    }

    public bool SaveFound()
    {
        // Check if both a save directory and file exist
        return Directory.Exists(_saveDirectory) && File.Exists(_saveFile);
    }

    public void Save(PlayerData data)
    {
        // Check if save directory exists
        if (!Directory.Exists(_saveDirectory))
        {
            // Create it if it doesn't
            Directory.CreateDirectory(_saveDirectory);
        }

        // Save data as JSON
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_saveFile, json);

        // Debug
        print("Save Successful!");
    }

    public void Load()
    {


        // Debug
        print("Save Successful!");
    }
}
