using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitButton : MonoBehaviour
{

    public progressManager progressManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        progressManagerRef = (progressManager) GameObject.FindObjectOfType<progressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logOutOfGame(){
        sendDataTest.isLoggingOut = true;
        progressManagerRef.saveGame();
        SceneManager.LoadScene("saveScene");
    }

    public void quitGame(){
        sendDataTest.isExiting = true;
        progressManagerRef.saveGame();
        SceneManager.LoadScene("saveScene");
    }
}
