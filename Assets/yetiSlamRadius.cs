using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yetiSlamRadius : MonoBehaviour
{
    // Start is called before the first frame update
   public yetiBoss wielder;
    

    public PlayerStats playerStats;

    
    void Start()
    {
        if (wielder == null)
        {
            wielder = GetComponentInParent<yetiBoss>();
        }

       playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            
            playerStats.takeDamage(wielder.damage);
            
        }
    }
}
