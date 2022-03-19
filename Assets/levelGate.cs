using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelGate : MonoBehaviour
{
    public BossQuest bossQuest;
    public PlayerStats playerStats;

    public int levelRestriction  = 2;
    public string nextLevel;
    public GameObject effect;
    public progressManager pg;
    public Collider col;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pg = GameObject.FindObjectOfType<progressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //level is met and boss quest is completed
        if(playerStats.level>=levelRestriction && bossQuest.Completed){
            effect.SetActive(true);
            col.enabled = true;
        }
        else{
            effect.SetActive(false);
            col.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other){
        pg.saveGame();
        SceneManager.LoadScene(nextLevel);
    }
    
}
