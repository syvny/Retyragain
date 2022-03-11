using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class progressManager : MonoBehaviour
{

    PlayerStats playerStats;
    
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

        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath +"/Khe Assets/JSON FILES/saveFile.json", json);

        //quest save to json once completed

   }

   public void loadGame(){
       string json = File.ReadAllText(Application.dataPath +"/Khe Assets/JSON FILES/saveFile.json");
       PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);

       playerStats.health = loadedPlayerData.playerHealth;
       playerStats.maxHealth = loadedPlayerData.playerMaxHealth;
       playerStats.mana = loadedPlayerData.playerMana;
       playerStats.maxMana = loadedPlayerData.playerMaxMana;
       playerStats.level = loadedPlayerData.playerLevel;
       PlayerStats.experience = loadedPlayerData.playerExperience;
       playerStats.healthPotions = loadedPlayerData.playerHpPotions;
       playerStats.manaPotions = loadedPlayerData.playerMpPotions;

       //load quest data in quest objects?
       
   }

    void OnApplicationQuit(){
        saveGame();
    }
    
}
