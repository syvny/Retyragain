using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatMenu : MonoBehaviour
{

    public string nextLevelString;
    public string prevLevelString;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fullHP(){
        playerStats.health = playerStats.maxHealth;
    }

    public void fullMP(){
        playerStats.mana = playerStats.maxMana;
    }

    public void nextLevel(){
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Level 3 Warrior Cheat" || scene.name == "Level 3 Mage Cheat"){

            Debug.Log("At last Level");

        }else{

            SceneManager.LoadScene(nextLevelString);

        }
        

    }

    public void prevLevel(){
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Level 1 Warrior Cheat" || scene.name == "Level 1 Mage Cheat"){

            Debug.Log("At first Level");

        }else{

            SceneManager.LoadScene(prevLevelString);

        }
        

    }
}
