using System.Collections;
using System.IO;
using _Scripts.ChestScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class SaveSystem
    {
        private static SaveData _saveData;
        [System.Serializable]
        public struct SaveData
        {
            public int sceneIndex;
            public PlayerSaveData playerData;
            public AmmoSaveData ammoData;
            public ChestOpening chestData;
            public KeyScript keyData;
            //zombie spawner and alive zombies
        }

        public static string SaveFileName()
        {
            string saveFileName = Application.persistentDataPath + "/save.save";
            return saveFileName;
        }

        public static void Save(int index)
        {
            HandleSaveData();
            _saveData.sceneIndex = index;
            File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData,true));
        }

        private static void HandleSaveData()
        {
            SaveLoadManager.Instance.player.Save(ref _saveData.playerData);
            SaveLoadManager.Instance.ammo.Save(ref _saveData.ammoData);
        }
        public static void Load()
        {
            Debug.Log("Loading Save");
            string saveContents = File.ReadAllText(SaveFileName());
            Debug.Log(saveContents);
            _saveData = JsonUtility.FromJson<SaveData>(saveContents);
            SceneManager.LoadScene(_saveData.sceneIndex);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log(saveContents);
        }
        
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SaveLoadManager.Instance.StartCoroutine(DelayedLoad());
        }
        
        private static IEnumerator DelayedLoad()
        {
            yield return null;                 // wait one frame
            yield return new WaitForEndOfFrame(); // wait movement scripts to initialize

            HandleLoadData();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private static void HandleLoadData()
        {
            
            SaveLoadManager.Instance.player.Load(_saveData.playerData);
            SaveLoadManager.Instance.ammo.Load(_saveData.ammoData);
            Debug.Log("Loaded Save");
        }
    
    }
}