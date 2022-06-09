using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    //leveling
    public int level = 1;

    public static int experience = 0;
    int maxExperience = 1000; //level threshold
    

    //Stats

    public int health = 100;
    public int maxHealth = 100;
    public int mana = 100;
    public int maxMana = 100;


    //Damage increases as player levels up
    public static int damage = 5;

    //Potions

    public int healthPotions = 0;

    public int manaPotions = 0;

    public TMP_Text healthPotionCount;
    public TMP_Text manaPotionCount;

    

    //max number of potions player can carry
    public int maxPotions = 10;


    //Effect values of potions here so i dont need to reference potions in player controller
    //Values are increased as player progresses

    public int healValue = 50;
    public int restoreValue = 50;

    public Animator animator;

    public static bool playerOnQuest = false;
    void Start()
    {
        damage = damage + (level * 20);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        healthPotionCount.text = healthPotions.ToString();
        manaPotionCount.text = manaPotions.ToString();
    
    }

    void UpdateStats(){
        
        //Health and mana
        if(health > maxHealth){

            health = maxHealth;
        }
        if(mana > maxMana){

            mana = maxMana;
        }

        //Potions
        if(healthPotions>=maxPotions){
            healthPotions=maxPotions;

        }
        if(healthPotions<=0){
            healthPotions = 0;
        }

        if(manaPotions>=maxPotions){
            manaPotions=maxPotions;
        }

        if(manaPotions<=0){
            manaPotions = 0;
        }

        if(experience>=maxExperience){
            level++;
            experience = 0;
        }
        if(experience<=0){
            experience = 0;
        }
    

    }

public void takeDamage(int damage){
    health -= damage;

        if(health<=0){
          
            PlayerController.isDead = true;
            
        }
}


  
}
