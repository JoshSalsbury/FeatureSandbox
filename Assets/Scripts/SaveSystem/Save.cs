using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{

    public static class SaveManager
    {

        public static event EventHandler OnSaveTriggered;
        
        private static class Constants
        {
            public static readonly string SaveFolderPath = Path.Combine(Application.dataPath, "Saves");
            public const int MaxSaveSlots = 3;
            public const string SaveFileBaseName = "save";
            public const string AutosaveFilename = "autosave";
            public const string SaveFileExtension = ".txt";
        }
        
        private static class SaveFolder
        {

            public static void EnsureSaveDirectory()
            {
                
                if (SaveFolderExists())
                {
                    Debug.Log("Save folder already exists");
                    return;
                }
                
                try
                {
                    Debug.Log("Creating save folder");
                    CreateSaveFolder();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error creating save folder at {Constants.SaveFolderPath}: {e}");
                }
                
            }
        
            private static bool SaveFolderExists()
            {
                return Directory.Exists(Constants.SaveFolderPath);
            }

            private static void CreateSaveFolder()
            {
                Directory.CreateDirectory(Constants.SaveFolderPath);
            }
        
        }

        public static void Save()
        {
            Debug.Log("Save function called.");
            SaveFolder.EnsureSaveDirectory();
            SaveObject saveObject = new(GameManager.Instance.CreateSaveData(), Player.Instance.CreateSaveData());
            string jsonSaveObject = SaveObjectToJson(saveObject);
            WriteSave(jsonSaveObject);
        }

        public static SaveObject Load()
        {
            
            if (!SaveExists())
            {
                return null;
            }

            return JsonStringToSaveObject(GetSaveString(GetSaveFilePath()));

        }

        private static bool SaveExists()
        {
            return File.Exists(GetSaveFilePath());
        }

        private static void WriteSave(string jsonSaveObject)
        {
            Debug.Log("Writing save");
            Debug.Log(jsonSaveObject);
            File.WriteAllText(GetSaveFilePath(),jsonSaveObject);
        }

        private static string GetSaveString(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        private static string GetSaveFilePath()
        {
            return Path.Combine(Constants.SaveFolderPath, GetSaveFileName());
        }

        private static string GetSaveFileName()
        {
            return "save.txt";
        }

        private static string SaveObjectToJson(SaveObject saveObject)
        {
            return JsonUtility.ToJson(saveObject);
        }

        private static SaveObject JsonStringToSaveObject(string json)
        {
            return JsonUtility.FromJson<SaveObject>(json);
        }
        
    }
    
}

namespace SaveSystem
{
    
    [Serializable]
    public class SaveObject
    {

        public Metadata metadata;
        public PlayerData playerData;

        public SaveObject(Metadata metadata, PlayerData playerData)
        {
            this.metadata = metadata;
            this.playerData = playerData;
        }
            
    }

    [Serializable]
    public class Metadata
    {
        
        public string id;
        public string name;
        public DateTime creationDateTime;

        public Metadata(string id, string name, DateTime creationDateTime)
        {
            this.id = id;
            this.name = name;
            this.creationDateTime = creationDateTime;
        }
        
    }
    
    [Serializable]
    public class PlayerData
    {
        
        public string name;
        public Vector3 position;

        public PlayerData(string name, Vector3 position)
        {
            this.name = name;
            this.position = position;
        }
        
    }
    
}
