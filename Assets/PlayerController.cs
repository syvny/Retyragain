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

    


    public FixedJoystick joystick;
    void Awake ()
	{
		// Set up the references.
		
		

	}

    private void Start(){
        cameraTransform = Camera.main.transform;
    }

    void Update(){

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

            animator.SetBool("MOVING", true);
            
       
          
   
        }
        else{
            
           animator.SetBool("MOVING", false);
            
        }

    }

  
    }


