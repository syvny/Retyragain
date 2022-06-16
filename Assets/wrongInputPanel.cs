using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongInputPanel : MonoBehaviour
{
    public GameObject panel;
    public static bool hasWrongPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasWrongPanel == true){
            panel.SetActive(true);
        }
    }

    public void setPanelOff(){
        hasWrongPanel = false;
    }
}
