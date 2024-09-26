
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SchoolRPG.Inventory.Runtime;
using SchoolRPG.NPC.Runtime;
using SchoolRPG.ProgressTracker.Runtime;

namespace SchoolRPG.SceneManagement.Runtime
{
    public class SaveData : MonoBehaviour
    {
        [SerializeField]
        private PlayerInventory playerInventory;

        [SerializeField]
        private Npc[] npcs;

        [SerializeField]
        private InventoryItem[] items;

        [SerializeField]
        private Progress_Tracker ProgressTracker;

        private string saveFilePath;
        private Data d;

        private static SaveData instance = null;


        [System.Serializable]
        public class Data
        {
            public List<InventoryItem> inventoryItems;
            public bool[] NPC_Pass;
            public bool[] Items_Collected;
            public bool[] tracker;
            public bool[] levelTracker;
        }

        // Start is called before the first frame update
        void Start()
        {
            SaveGame();
        }

        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame()
        {
            d = new Data();
            d.NPC_Pass = new bool[npcs.Length];
            d.Items_Collected = new bool[items.Length];
            d.inventoryItems = playerInventory.inventoryItems;
            d.tracker = ProgressTracker.tracker;
            d.levelTracker = ProgressTracker.levelTracker;
            for (int i = 0; i < npcs.Length; i++)
                d.NPC_Pass[i] = npcs[i].IsPassed;
            for (int i = 0;i < items.Length; i++)
                d.Items_Collected[i] = items[i].Collected;

            saveFilePath = Application.dataPath + "/Saves/";

            string saveData = JsonUtility.ToJson(d);

            File.WriteAllText(saveFilePath + "/SaveData.json", saveData);
        }

        public void LoadGame()
        {
            Debug.Log("Loading save...");
            if (File.Exists(saveFilePath + "/SaveData.json"))
            {
                string loadPlayerData = File.ReadAllText(saveFilePath + "/SaveData.json");
                JsonUtility.FromJsonOverwrite(loadPlayerData, d);

                playerInventory.inventoryItems = d.inventoryItems;
                ProgressTracker.tracker = d.tracker;
                ProgressTracker.levelTracker = d.levelTracker;

                for (int i = 0; i < d.NPC_Pass.Length; i++)
                    npcs[i].IsPassed = d.NPC_Pass[i];
                for (int i = 0; i < d.Items_Collected.Length; i++)
                    items[i].Collected = d.Items_Collected[i];
            }
        }

        public void DeleteSave()
        {
            Debug.Log("Deleting save...");
            if (File.Exists(saveFilePath + "/SaveData.json"))
            {
                File.Delete(saveFilePath + "/SaveData.json");
            }
        }
    }
}