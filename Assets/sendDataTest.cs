using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using progManager;
using System.Runtime.Serialization.Formatters.Binary;


public class sendDataTest : MonoBehaviour
{
    public Web web;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main.Instance.Web.sendSaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
