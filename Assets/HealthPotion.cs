using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerStats playerStats;
    public int healValue = 50;


    void Start()
    {
         playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerStats.healthPotions++;
            Destroy(gameObject);
        }
    }
}
