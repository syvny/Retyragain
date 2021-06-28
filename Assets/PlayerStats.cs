using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update


    public static PlayerStats playerStats;

    public int level = 1;

    public int health = 100;
    public int maxHealth = 100;
    public int mana = 100;
    public int maxMana = 100;

    public int strength = 1;
    public int intelligence = 1;

    public int statPoints = 0;


    public int damage;

    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateLife(){

        if(health > maxHealth){

            health = maxHealth;
        }
    }
}
