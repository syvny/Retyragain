using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySword : MonoBehaviour
{
    // Start is called before the first frame update

    public Enemy wielder;
    

    public PlayerStats playerStats;

    
    void Start()
    {
        if (wielder == null)
        {
            wielder = GetComponentInParent<Enemy>();
        }

       playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            
            playerStats.takeDamage(wielder.damage);
            
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            playerStats.takeDamage(wielder.damage);
            Debug.Log("Player got hit");
        }
    }
}
