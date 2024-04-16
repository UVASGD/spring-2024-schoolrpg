
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    public GameObject player;
    public GameObject[] mobs;
    public GameObject[] npcs;

    private string saveFilePath;
    private PlayerData pd;
    private MobData md;
    private NPCData nd;

    [Serializable]
    public class MobCoords : SerializableDictionaryBase<GameObject, Vector3> { }
    [Serializable]
    public class MobHealth : SerializableDictionaryBase<GameObject, int> { }
    [Serializable]
    public class NPCCoords : SerializableDictionaryBase<GameObject, Vector3> { }


    [System.Serializable]
    public class PlayerData
    {
        public Vector3 playerCoords = new Vector3(0,0,0);
        public int playerHealth;
        public int[] playerInventory; // replace int with whatever object the items are
        public int currentLevel;
        public int playerStorySection; // replace int with whatever object I decide to use for the story
    }

    [System.Serializable]
    public class MobData
    {
        public MobCoords mobCoords = new MobCoords();
        public MobHealth mobHealth = new MobHealth();
    }

    [System.Serializable]
    public class NPCData
    {
        public NPCCoords npcCoords = new NPCCoords();
        // perhaps some future dialogue save?
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        pd = new PlayerData();
        pd.playerCoords = player.transform.position;
        pd.playerHealth = 5;// replace with player.healthvalue or whatever it will be called
        pd.playerInventory = new int[]{5,5,5};// replace with whatver implementation of inventory there is
        pd.currentLevel = 1;
        pd.playerStorySection = 1;// placeholder values honestly except for coords

        md = new MobData();

        for (int i = 0; i < mobs.Length; i++)
        {
            md.mobCoords.Add(mobs[i], mobs[i].transform.position);
            //md.mobHealth[mobs[i]] = mobs[i].health; Health value unused right now
        }

        nd = new NPCData();
        for (int i = 0; i < npcs.Length; i++)
        {
            nd.npcCoords.Add(npcs[i], npcs[i].transform.position);
        }

        saveFilePath = Application.dataPath + "/Saves/";

        string savePlayerData = JsonUtility.ToJson(pd);
        string saveMobData = JsonUtility.ToJson(md);
        string saveNPCData = JsonUtility.ToJson(nd);

        File.WriteAllText(saveFilePath + "/PlayerData.json", savePlayerData);
        File.WriteAllText(saveFilePath + "/MobData.json", saveMobData);
        File.WriteAllText(saveFilePath + "/NPCData.json", saveNPCData);
    }

    public void LoadGame()
    {
        Debug.Log("Loading save...");
        if (File.Exists(saveFilePath + "/PlayerData.json"))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath + "/PlayerData.json");
            JsonUtility.FromJsonOverwrite(loadPlayerData, pd);

            player.transform.position = pd.playerCoords;
            // put rest of player data from pd into player gameobject
        }
        if (File.Exists(saveFilePath + "/MobData.json"))
        {
            string loadMobData = File.ReadAllText(saveFilePath + "/MobData.json");
            JsonUtility.FromJsonOverwrite(loadMobData, md);

            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].transform.position = md.mobCoords[mobs[i]];
                // health, etc...
            }
        }
        if (File.Exists(saveFilePath + "/NPCData.json"))
        {
            string loadNPCData = File.ReadAllText(saveFilePath + "/NPCData.json");
            JsonUtility.FromJsonOverwrite(loadNPCData, nd);

            for (int i = 0; i < npcs.Length; i++)
            {
                npcs[i].transform.position = nd.npcCoords[npcs[i]];
            }
        }
    }

    public void DeleteSave()
    {
        Debug.Log("Deleting save...");
        if (File.Exists(saveFilePath + "/PlayerData.json"))
        {
            File.Delete(saveFilePath + "/PlayerData.json");
        }
        if (File.Exists(saveFilePath + "/MobData.json"))
        {
            File.Delete(saveFilePath + "/MobData.json");
        }
        if (File.Exists(saveFilePath + "/NPCData.json"))
        {
            File.Delete(saveFilePath + "/NPCData.json");
        }

    } 
}
