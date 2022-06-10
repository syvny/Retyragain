using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retrieveSaveFiles : MonoBehaviour
{

    public Web web;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main.Instance.Web.retrieveSaveFile());
        StartCoroutine(Main.Instance.Web.retrieveQuestFile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
