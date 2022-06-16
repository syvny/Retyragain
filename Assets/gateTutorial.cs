using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateTutorial : MonoBehaviour
{
    public BossQuest quest;
    public GameObject gateTutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(quest.Completed == true){
            gateTutorialPanel.SetActive(true);
        }
    }
}
