using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class statsPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text health;
    public TMP_Text mana;
    public TMP_Text level;
    public TMP_Text experience;

    public PlayerStats playerStats;

    public GameObject questPanel;
    public GameObject stats;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        closeWhen();
        health.text = "Health: " + playerStats.health;
        mana.text = "Mana: "+playerStats.mana;
        level.text = "Level: "+ playerStats.level;
        experience.text = "Exp: " + PlayerStats.experience;
    }

    void closeWhen(){

        if(questPanel.activeInHierarchy == true){
            stats.SetActive(false);
        }
        else{
            stats.SetActive(true);
        }
    }
}
