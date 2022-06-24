using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class statsHome : MonoBehaviour
{
    public Web web;
    public TMP_Text usernameText;
    public TMP_Text levelText;
    public TMP_Text classText;

    public static bool isNewPlayer = false;

    void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main.Instance.Web.getClass());
        StartCoroutine(Main.Instance.Web.getLevel());
        StartCoroutine(waiter());

        string first = PlayerPrefs.GetString("level");
        string second = PlayerPrefs.GetString("class");
        usernameText.text = usernameText.text + " " + PlayerPrefs.GetString("username");
        Debug.Log("Level " + first);
        levelText.text = levelText.text + " " + first;
        classText.text = classText.text + " " + second;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){

        if(isNewPlayer == true){
            
            SceneManager.LoadScene("Opening Scene");
        }else{
            string savePath = Application.persistentDataPath + "/saveFile.txt";
            string questPath = Application.persistentDataPath + "/questProgress.txt";
            string someString = "Level " + PlayerPrefs.GetString("level") + " " + PlayerPrefs.GetString("class");
            SceneManager.LoadScene(someString);
        }
        
    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(5);
        
    }

}
