using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    

    //Buttons

    public FixedButton attackButton;
    public FixedButton dashButton;
    public FixedButton specialAttackButton;

    public FixedButton useHealthPotion;
    public FixedButton useManaPotion;

    //Ref Player Stats

    public PlayerStats playerStats;


//Attacking
    public static bool attacking;
    public float attackTimer;

     public bool canMove;

//Dashing
    public bool dashing;

    float dashTimer;

//Has Killed Enemy ID of recent kill
    public static int hasKilled;


    


    public FixedJoystick joystick;
    void Awake ()
	{
		// Set up the references.
		
		

	}

    private void Start(){
        cameraTransform = Camera.main.transform;
    }

    void Update(){

        
        

     
        canMove=true;

        //using Potions

        if(useHealthPotion.Pressed && playerStats.healthPotions>=1){
            playerStats.health+=playerStats.healValue;
            playerStats.healthPotions--;

        }
   
        if(useManaPotion.Pressed && playerStats.manaPotions>=1){
            playerStats.mana+=playerStats.restoreValue;
            playerStats.manaPotions--;

        }
        


        //attacking
        
        if (attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                 Debug.Log("Attacking False");
                attacking = false;
                
               
            }
            canMove=false;
        
        }
        else if (attackButton.Pressed)
        {
            Debug.Log("Attacking true");
            attacking = true;
            attackTimer = 1.2f;
            canMove=false;

            animator.SetBool("MOVING", false);
            animator.SetTrigger("Attack Sword");
            
        }
         else if (specialAttackButton.Pressed)
        {
            //spin attack still not working
            attacking = true;
            attackTimer = 2f;
            canMove=false;
            animator.SetBool("MOVING", false);
            animator.SetTrigger("Special Attack");
        }


        if(canMove){
            
              if(dashing){
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                     {
                 
                    dashing = false;
                    MoveSpeed -= 10f;
                    
                    }
                
                
            }
            else if(dashButton.Pressed){
                Debug.Log("Dahs");
                dashing=true;
                MoveSpeed +=10f;
                dashTimer = 3f;
            }
           
            Movement();
        }

       
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

}

