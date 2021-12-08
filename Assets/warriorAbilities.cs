using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warriorAbilities : MonoBehaviour
{

    public PlayerController playerController;
    public PlayerStats playerStats;
    public Animator animator;
    public FixedButton attackButton;
    public FixedButton dashButton;
    public FixedButton specialAttackButton;

    public GameObject sword;

    public GameObject specialAttackRadius;

    public float attackTimer;
    public bool canMove;
    public bool dashing;
    //Dashing
 
    public float dashTimer;

    //cooldowns 

    public float specialAttackCooldown;
    public float dashCooldown;

    public bool canSpecialAttack;
    public bool canDash;

    public Image specialAttackImage;
    public Image dashImage;


    public ParticleSystem burst;
    public AudioSource slashSound;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
         //attacking

         if(specialAttackCooldown<=0){
             canSpecialAttack = true;
         }
         else{
             canSpecialAttack = false;
         }

         if(dashCooldown<=0){
             canDash = true;
         }
         else{
             canDash = false;
         }
        
        if (PlayerController.attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                
                PlayerController.attacking = false;
                
               
            }
            canMove=false;
        
        }
        else if (attackButton.Pressed)
        {
            
            PlayerController.attacking = true;
            attackTimer = 1.2f;
            canMove=false;

            animator.SetBool("MOVING", false);
            animator.SetTrigger("Attack Sword");
            
        }
         else if (specialAttackButton.Pressed)
        {
            

            if(canSpecialAttack && playerStats.mana>=50){ //not on cooldown
                PlayerController.attacking = true;
                playerStats.mana-=40;
                attackTimer = 2f;
                specialAttackCooldown = 7f;
                canMove=false;
                animator.SetBool("MOVING", false);
                animator.SetTrigger("Special Attack");
            }
            
        }

        if(specialAttackCooldown>=0){
            specialAttackImage.fillAmount = 1/specialAttackCooldown;
        }
        else{
            specialAttackImage.fillAmount = 1;
        }
        specialAttackCooldown-=Time.deltaTime;


        if(playerController.canMove){
            
              if(playerController.dashing){
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                     {
                 
                    playerController.dashing = false;
                    playerController.MoveSpeed -= 7f;
                    
                    }
                
                
            }
            else if(dashButton.Pressed){

                if(canDash && playerStats.mana>=20){ //not on cooldown
                  
                     playerStats.mana-=20;
                   Debug.Log("Dahs");
                 playerController.dashing=true;
                playerController.MoveSpeed +=7f;
                dashCooldown = 10f;
                dashTimer = 3f;
                    
            }
            
        }


            }
            if(dashCooldown>=0){
            dashImage.fillAmount = 1/dashCooldown;
        }
        else{
            dashImage.fillAmount = 1;
        }
       dashCooldown-=Time.deltaTime;
    }
    

    public void activateSword(){
        sword.GetComponent<Collider>().enabled = true;
    }

    public void deactivateSword(){
        sword.GetComponent<Collider>().enabled = false;
    }

    public void activateSpecialAttack(){
        //special Attack
        specialAttackRadius.GetComponent<Collider>().enabled = true;
    }

    public void deactivateSpecialAttack(){
         specialAttackRadius.GetComponent<Collider>().enabled = false;
    }

    public void burstEffect(){
        //burst effect animation event
        burst.Play();
        
        
    }

    public void slashEffect(){

        slashSound.Play();
    }
}
