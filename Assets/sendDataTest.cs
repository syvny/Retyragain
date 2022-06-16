using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using progManager;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public class sendDataTest : MonoBehaviour
{
    public Web web;
    public static bool isLoggingOut = false;
    public static bool isExiting = false;
    public static bool isEndGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if(isLoggingOut == true){            
            StartCoroutine(Main.Instance.Web.sendSaves());
            StartCoroutine(Main.Instance.Web.setLevelAndExp());
            isLoggingOut = false;
            
            PlayerPrefs.DeleteAll();
            StartCoroutine(waiter());
            
        }
        else if(isExiting == true){
            isExiting = false;
            StartCoroutine(Main.Instance.Web.sendSaves());
            PlayerPrefs.DeleteAll();
            Application.Quit();

        }
        else if(isEndGame == true){
            isEndGame = false;
            StartCoroutine(Main.Instance.Web.sendSaves());
            SceneManager.LoadScene("Ending Scene");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main");
    }
}
