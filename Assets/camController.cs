using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
    // Start is called before the first frame update



    public GameObject target;
    public int height =100;

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {

      
        transform.position = new Vector3(target.transform.position.x,target.transform.position.y + height,target.transform.position.z -10 );
    }
}
