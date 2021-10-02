using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossSword : MonoBehaviour
{
    // Start is called before the first frame update
   public SkeletonKingBoss wielder;
    

    public PlayerStats playerStats;

    
    void Start()
    {
        if (wielder == null)
        {
            wielder = GetComponentInParent<SkeletonKingBoss>();
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
}
