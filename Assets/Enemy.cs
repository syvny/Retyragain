using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public PlayerStats playerStats;
    public PlayerController player;
  

    
    public int health = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Put enemy AI here
        UpdateHealth();
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "playerAttack" && player.attacking == true){
            //health - playerStats damage

            health -= playerStats.damage;
        }

    }

    void UpdateHealth(){

        if(health<=0){
            Destroy(gameObject);

        }
    }
}
