using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcomeScreen : MonoBehaviour
{

    public GameObject welcomeScreenObject;
    // Start is called before the first frame update
    void Start()
    {
        if(statsHome.isNewPlayer == true){
            welcomeScreenObject.SetActive(true);
        }
        else{
            welcomeScreenObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNewPlayerOff(){
        statsHome.isNewPlayer =false;
    }
}
