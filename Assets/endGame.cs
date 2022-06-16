using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{

    public BossQuest quest24;
    public progressManager progressMan;
    // Start is called before the first frame update
    void Start()
    {
        progressMan = FindObjectOfType<progressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(quest24.Completed == true){
            progressMan.saveGame();
            sendDataTest.isEndGame = true;
            SceneManager.LoadScene("saveScene");
        }
    }
}
