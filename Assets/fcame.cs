using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fcame : MonoBehaviour
{
    public Camera[] cams;
    // Start is called before the first frame update
    void Start()
    {
        cams = GameObject.FindObjectsOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
