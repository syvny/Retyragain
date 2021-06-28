using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
    // Start is called before the first frame update

    public float Xaxis;
    public float Yaxis;

    public float RotationSensitivity = 4f;
    public float RotationMin = -40f;
    public float RotationMax = 80f;
    float smoothtime = 0.12f;

    public bool mobileControls = false;
    public FixedTouchField touchField;

    public Transform target;
    Vector3 currentvel;


    Vector3 targetRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(mobileControls){
        Yaxis += touchField.TouchDist.x *RotationSensitivity;
        Xaxis -= touchField.TouchDist.y *RotationSensitivity;

        }else{
        Yaxis += Input.GetAxis("Mouse X")*RotationSensitivity;
        Xaxis -= Input.GetAxis("Mouse Y")*RotationSensitivity;

        }
    

        Xaxis= Mathf.Clamp(Xaxis,RotationMin,RotationMax);

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis,Yaxis),ref currentvel,smoothtime);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * 5f;


    }
}
