using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MoveSpeed = 6f;
    float currentVelocity;
    public float smoothRotationTime = 0.25f;
    public bool mobileControls = false;


    float currentSpeed;
    float speedVelocity;

    Transform cameraTransform;

    public Animator animator;

    Transform swordHand;

    public FixedButton attackButton;

    public bool attacking;
    public float attackTimer;

     public bool canMove;

    public bool dashing;

    float dashTimer;


    


    public FixedJoystick joystick;
    void Awake ()
	{
		// Set up the references.
		
		

	}

    private void Start(){
        cameraTransform = Camera.main.transform;
    }

    void Update(){

        
        animator.SetBool("Normal Attack (Warrior)", false);
        animator.SetBool("Strong Hit",false);

     
        canMove=true;


        //attacking
        
        if (attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                 
                attacking = false;
                
               
            }
            canMove=false;
        
        }
        else if (attackButton.Pressed)
        {
            attacking = true;
            attackTimer = 1.2f;
            canMove=false;

            animator.SetBool("MOVING", false);
            animator.SetBool("Normal Attack (Warrior)",true);
            
        }
         else if (Input.GetKey(KeyCode.F))
        {
            //spin attack still not working
            attacking = true;
            attackTimer = 2f;
            canMove=false;
            animator.SetBool("MOVING", false);
            animator.SetBool("Strong Hit",true);
        }


        if(canMove){
            
           
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
        transform.Translate(transform.forward *targetSpeed * Time.deltaTime,Space.World);

         if(inputDir.magnitude>0){

             //check dash (add cooldown to dash) no cooldown yet

              if(dashing){
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                     {
                 
                    dashing = false;
                    MoveSpeed -= 10f;
                    
                    }
                
                
            }
            else if(Input.GetKey(KeyCode.G)){
                dashing=true;
                MoveSpeed +=10f;
                dashTimer = 3f;
            }
          animator.SetBool("MOVING", true);
                

   
        }
        else{
            
           animator.SetBool("MOVING", false);
          
            
        }


  
    }

}

