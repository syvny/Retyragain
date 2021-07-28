using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public PlayerStats playerStats;
    public PlayerController player;
  

    //Dropable Items

    public GameObject[] dropables;
    //Dropable item spawn position
    public GameObject dropPosition;

    //Sight and Attack Ranges
   

    
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
            Debug.Log("Hit");
            //health - playerStats damage

            health -= playerStats.damage;
        }

    }

    void UpdateHealth(){

        if(health<=0){
            Die();

        }
    }

    void Die(){
        //check if array is empty
        if(dropables[0]!=null){
            //random number generator
            var i = Random.Range(0,10);
            Debug.Log(i);
            if(i<=dropables.Length){
                var dropSpawn = (GameObject) Instantiate(dropables[i],dropPosition.transform.position,dropPosition.transform.rotation);

            }

        }
        Destroy(gameObject);

    }
}
