using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject creationPanel;
    
    

    void Start(){
     
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
}
