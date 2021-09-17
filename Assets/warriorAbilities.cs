using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warriorAbilities : MonoBehaviour
{

    public PlayerController playerController;
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

    public Image specialAttackImage;
    public Image dashImage;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         //attacking
        
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
            

            if(specialAttackCooldown<=0){ //not on cooldown
                PlayerController.attacking = true;
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


        if(canMove){
            
              if(dashing){
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                     {
                 
                    dashing = false;
                    playerController.MoveSpeed -= 10f;
                    
                    }
                
                
            }
            else if(dashButton.Pressed){
                Debug.Log("Dahs");
                dashing=true;
                playerController.MoveSpeed +=10f;
                dashTimer = 3f;
            }
    }
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
}
