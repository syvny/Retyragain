using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerStats playerStats;
    public int restoreValue = 50;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerStats.mana+=restoreValue;
            Destroy(gameObject);
        }
    }
}
