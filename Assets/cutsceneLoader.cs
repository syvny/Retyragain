using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //We have to get the class first
        SceneManager.LoadScene("Level 1 Warrior Cheat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
