using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //Movement Variables
    public bool mobileControls = false;
    public float MoveSpeed = 6f;
    float currentVelocity;
    public float smoothRotationTime = 0.25f;
    
    float currentSpeed;
    float speedVelocity;

    public Rigidbody rb;



    //Cam
    Transform cameraTransform;

    public Animator animator;

    Transform swordHand;


    //Potions
    public FixedButton useHealthPotion;
    public Image healthpotionImage;
    public FixedButton useManaPotion;
    public Image manapotionImage;

    //Cooldown Values
    public float healthpotionCooldown = 10f;
    float manapotionCooldown = 10f;

    //Ref Player Stats

    public PlayerStats playerStats;


    //Respawn

    public GameObject respawnPanel;


//Attacking
    public static bool attacking;
    public float attackTimer;

     public bool canMove;

     public bool dashing;



//Has Killed Enemy ID of recent kill
    public static int hasKilled;

//Respawn Position
    public static Transform playerRespawnPosition;

//Dead
    public static bool isDead =false;


    


    public FixedJoystick joystick;
    void Awake ()
	{
		// Set up the references.
		
		

	}

    private void Start(){
        cameraTransform = Camera.main.transform;
    }

    void Update(){


        
        
        if(isDead){
            canMove=false;
            animator.SetTrigger("DEAD");
            respawnPanel.SetActive(true);
        }
        else{
            canMove=true;
        }

     
        

        //using Potions

        if(useHealthPotion.Pressed && playerStats.healthPotions>=1){
            if(healthpotionCooldown<=0){
                //not on cooldown
                healSelf();
                healthpotionCooldown = 10f;
            }
        }

        if(healthpotionCooldown>=0){
            healthpotionImage.fillAmount = 1/healthpotionCooldown;
        }
        else{
            healthpotionImage.fillAmount = 1;
        }
        healthpotionCooldown-=Time.deltaTime;


   
        if(useManaPotion.Pressed && playerStats.manaPotions>=1){
            if(manapotionCooldown<=0){
                restoreMana();
                manapotionCooldown  = 10f;
            }
            
        }
        if(manapotionCooldown>=0){
            manapotionImage.fillAmount = 1/manapotionCooldown;
        }
        else{
            manapotionImage.fillAmount = 1;
        }
        manapotionCooldown-=Time.deltaTime;
        


       


        
           
            Movement();
        

       
    }

    void healSelf(){
         playerStats.health+=playerStats.healValue;
         playerStats.healthPotions--;
    }

    void restoreMana(){
             playerStats.mana+=playerStats.restoreValue;
            playerStats.manaPotions--;
    }

    void Movement(){

         Vector2 input = Vector2.zero;

        
        if(mobileControls){
            input = new Vector2(joystick.Horizontal, joystick.Vertical);


        }

        else{

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        
        Vector2 inputDir = input.normalized;

        if(inputDir!=Vector2.zero){
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,rotation, ref currentVelocity, smoothRotationTime);
        }
        float targetSpeed = (MoveSpeed * inputDir.magnitude);
        currentSpeed = Mathf.SmoothDamp(currentSpeed,targetSpeed,ref speedVelocity,0.1f);

       // transform.Translate(transform.forward *targetSpeed * Time.deltaTime,Space.World);
       Vector3 I =  new Vector3(joystick.Horizontal * MoveSpeed,0,joystick.Vertical * MoveSpeed);
       

        rb.velocity =  I;
        

         if(inputDir.magnitude>0){

             //check dash (add cooldown to dash) no cooldown yet

            
          animator.SetBool("MOVING", true);
                

   
        }
        else{
            
           animator.SetBool("MOVING", false);
          
            
        }


  
    }


    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "enemyAttack"){
            
            animator.SetTrigger("Get Hit");

        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Respawn Checkpoint"){
            Debug.Log("Respawn ");
            playerRespawnPosition = other.transform;
        }   
        if(other.tag == "enemyAttack"){
            
            animator.SetTrigger("Get Hit");

        }
    }

    public void playerRespawn(){
        transform.position = playerRespawnPosition.transform.position;
        isDead =false;
      
        //experience penalty
        playerStats.health = playerStats.maxHealth;
        playerStats.mana = playerStats.maxMana;
        PlayerStats.experience-=10;
        animator.ResetTrigger("DEAD");
        animator.SetBool("IDLE",true);
        respawnPanel.SetActive(false);
    }
}
