using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{

    public GameObject creationPanel;
    
    

    void Start(){
        Screen.SetResolution(1280, 720, true);
    }
    // Start is called before the first frame update
  public void StartGame(){
      creationPanel.SetActive(true);
  }

  public void QuitGame(){
      Application.Quit();
      Debug.Log("Quit Game");
  }

  public void CreateWarrior(){
      SceneManager.LoadScene("Level 1 Warrior Cheat");
  }

  public void CreateMage(){
      SceneManager.LoadScene("Level 1 Mage Cheat");

  }

  public void clearSaves(){
      string path = Application.persistentDataPath + "/saveFile.txt";
       if(File.Exists(path)){
           File.Delete(path);
            Debug.Log("Save File Deleted");
       }else{

           Debug.Log("Save File not found in "+ path);
           
       }

       string qPath = Application.persistentDataPath+"/questProgress.txt";

       if(File.Exists(qPath)){
            File.Delete(qPath);
            Debug.Log("Quest Progress Deleted");
            
        }else{

            Debug.Log("Quest Progress not found in "+ path);

        }
  }
}
