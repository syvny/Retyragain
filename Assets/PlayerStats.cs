using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update


    


    //leveling
    public int level = 1;

    public static int experience = 0;
    public int maxExperience;

    //Stats

    public int health = 100;
    public int maxHealth = 100;
    public int mana = 100;
    public int maxMana = 100;


    //Damage increases as player levels up
    public static int damage = 25;

    //Potions

    public int healthPotions;

    public int manaPotions;

    //max number of potions player can carry
    public int maxPotions = 10;


    //Effect values of potions here so i dont need to reference potions in player controller
    //Values are increased as player progresses

    public int healValue = 50;
    public int restoreValue = 50;

    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();

    
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
        if(healthPotions>maxPotions){
            healthPotions=maxPotions;

        }

        if(manaPotions>maxPotions){
            manaPotions=maxPotions;
        }

        //Levels

        if(experience>=maxExperience){

            //reset experience
            experience = experience - maxExperience;
            //maxexp = maxexp*1.5
            maxExperience = maxExperience + 100;
            //leveled up
            level++;
            damage+=10;
            maxHealth+=50;
            maxMana+=50;
        }

     

    }

public void takeDamage(int damage){
    health -= damage;

        if(health<=0){
          
            PlayerController.isDead = true;
            animator.SetTrigger("DEAD");
        }
}
  
}
