using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class quitFromMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quit(){

        string savePath = Application.persistentDataPath + "/saveFile.txt";
        string questPath = Application.persistentDataPath + "/questProgress.txt";

        if(File.Exists(savePath) && File.Exists(questPath)){
            File.Delete(savePath);
            File.Delete(questPath);
        }
        StartCoroutine(waiter());
        Application.Quit();
        Debug.Log("Quit Game");
    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(5);
    }
}
