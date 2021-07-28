using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceCrystal : MonoBehaviour
{
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        playerStats.maxMana+=100;

    }
}
