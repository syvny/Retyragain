using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class progressManager : MonoBehaviour
{

    PlayerStats playerStats;

 
    //Before all of this, must overwrite saveFile from database at path persistentData


    // Start is called before the first frame update
    void Start()
    {

        //Find PlayerStats
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Debug.Log("Start");

        //on Start Load Game
        loadGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class PlayerData{

        
        public int playerHealth;
        public int playerMaxHealth;
        public int playerMana;
        public int playerMaxMana;
        public int playerLevel;
        public int playerExperience;

        public int playerHpPotions;
        public int playerMpPotions;

        
    }

   
   public void saveGame(){

       BinaryFormatter formatter = new BinaryFormatter();
       string path = Application.persistentDataPath + "/saveFile.txt";
       FileStream stream = new FileStream(path, FileMode.Create);
       //playerdata save
        PlayerData playerData = new PlayerData();

        playerData.playerHealth = playerStats.health;
        playerData.playerMaxHealth = playerStats.maxHealth;
        playerData.playerMana = playerStats.mana;
        playerData.playerMaxMana = playerStats.maxMana;
        playerData.playerLevel = playerStats.level;
        playerData.playerExperience = PlayerStats.experience;
        playerData.playerHpPotions = playerStats.healthPotions;
        playerData.playerMpPotions = playerStats.manaPotions;

        formatter.Serialize(stream, playerData);
        stream.Close();

        

        

        // string json = JsonUtility.ToJson(playerData);

        // File.WriteAllText(Application.dataPath +"/Khe Assets/JSON FILES/saveFile.txt", json);

        //quest save to json once completed

   }

   public void loadGame(){
       string path = Application.persistentDataPath + "/saveFile.txt";
       if(File.Exists(path)){
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);

        PlayerData loadedPlayerData = formatter.Deserialize(stream) as PlayerData;

        playerStats.health = loadedPlayerData.playerHealth;
        playerStats.maxHealth = loadedPlayerData.playerMaxHealth;
        playerStats.mana = loadedPlayerData.playerMana;
        playerStats.maxMana = loadedPlayerData.playerMaxMana;
        playerStats.level = loadedPlayerData.playerLevel;
        PlayerStats.experience = loadedPlayerData.playerExperience;
        playerStats.healthPotions = loadedPlayerData.playerHpPotions;
        playerStats.manaPotions = loadedPlayerData.playerMpPotions;

        stream.Close();
           
       }else{

           Debug.Log("Save File not found in "+ path);
           
       }


    //    string json = File.ReadAllText(Application.dataPath +"/Khe Assets/JSON FILES/saveFile.txt");
    //    PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);

      

       //load quest data in quest objects?
       
   }

    void OnApplicationQuit(){
        saveGame();
    }

    public void saveAndQuit(){
        saveGame();
        Application.Quit();
        Debug.Log("Saved and exited");

        //Send to database
    }
    
}
