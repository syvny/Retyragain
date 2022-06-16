using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerExceptions : MonoBehaviour
{
    public static bool registerException;
    public GameObject registerExceptionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(registerException == true){
            registerExceptionPanel.SetActive(true);
            
        }
    }

    public void setRegisterExceptionToFalse(){
        registerException = false;
    }
    
}
