using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mageAbilities : MonoBehaviour
{


    public PlayerController playerController;
    public PlayerStats playerStats;
    public Animator animator;

    //projectiles

    public GameObject fireball;
    public GameObject snowball;
    public GameObject burstAttackRadius;


    //buttons
    public FixedButton attackButton;
    public FixedButton burstAttackButton;
    public FixedButton slowAttackButton;
     public float attackTimer;
    public bool canMove;

     public float burstAttackCooldown;
    public float slowAttackCooldown;

    public bool canBurstAttack;
    public bool canSlowAttack;

    public Image burstAttackImage;
    public Image slowAttackImage;

    //particle effects
    public ParticleSystem burst;

    //SFX
    public AudioSource fireballSound;
    public AudioSource snowballSound;
    

    //position

    public GameObject projectilePosition;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(burstAttackCooldown<=0){
            canBurstAttack = true;
        }
        else{
            canBurstAttack = false;
        }

        if(slowAttackCooldown<=0){
            canSlowAttack = true;
        }
        else{
            canSlowAttack = false;
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
            animator.SetTrigger("Fireball");
            
        }
         else if (burstAttackButton.Pressed)
        {
            

            if(canBurstAttack && playerStats.mana>=50){ //not on cooldown
                PlayerController.attacking = true;
                playerStats.mana-=40;
                attackTimer = 2f;
                burstAttackCooldown = 7f;
                canMove=false;
                animator.SetBool("MOVING", false);
                animator.SetTrigger("Burst Attack");
            }
            
        }
        else if(slowAttackButton.Pressed){
              if(canSlowAttack && playerStats.mana>=50){ //not on cooldown
                PlayerController.attacking = true;
                playerStats.mana-=40;
                attackTimer = 2f;
                slowAttackCooldown = 7f;
                canMove=false;
                animator.SetBool("MOVING", false);
                animator.SetTrigger("Snowball"); //Same animation as normal attack
            }
            
        }

        if(slowAttackCooldown>=0){
            slowAttackImage.fillAmount = 1/slowAttackCooldown;
        }
        else{
            slowAttackImage.fillAmount = 1;
        }
        slowAttackCooldown-=Time.deltaTime;

     

        if(burstAttackCooldown>=0){
            burstAttackImage.fillAmount = 1/burstAttackCooldown;
        }
        else{
            burstAttackImage.fillAmount = 1;
        }
        burstAttackCooldown-=Time.deltaTime;


      
    }

    void launchFireball(){
        var proj = (GameObject )Instantiate(fireball,projectilePosition.transform.position,projectilePosition.transform.rotation);
    }

    void launchSnowball(){
         var proj = (GameObject )Instantiate(snowball,projectilePosition.transform.position,projectilePosition.transform.rotation);
    }

    void burstAttackActivate(){
        burstAttackRadius.GetComponent<Collider>().enabled = true;
        //double damage attack
    }

    void burstAttackdeActivate(){
        burstAttackRadius.GetComponent<Collider>().enabled = false;
    }

    public void playBurst(){
        burst.Play();
    }

    public void playFireballSound(){
        fireballSound.Play();
    }

    public void playSnowballSound(){
        snowballSound.Play();
    }
}
