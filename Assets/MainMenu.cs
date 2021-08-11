using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject creationPanel;
    // Start is called before the first frame update
  public void StartGame(){
      creationPanel.SetActive(true);
  }

  public void QuitGame(){
      Application.Quit();
  }

  public void CreateCharacter(){
      SceneManager.LoadScene("Level 1");
  }
}
