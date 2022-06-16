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

        string lev = PlayerPrefs.GetString("level");
        string cla = PlayerPrefs.GetString("class");
        SceneManager.LoadScene("Level "+ lev + " " + cla);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
