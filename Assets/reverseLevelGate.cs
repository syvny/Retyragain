using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class reverseLevelGate : MonoBehaviour
{
    
    public PlayerStats playerStats;

    public string pastLevel;
    
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
        
    }

    void OnTriggerEnter(Collider other){
        pg.saveGame();
        SceneManager.LoadScene(pastLevel);
    }
    
}
