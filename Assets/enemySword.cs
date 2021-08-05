using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySword : MonoBehaviour
{
    // Start is called before the first frame update

    public Enemy wielder;
    public GameObject edge;
    void Start()
    {
        if (wielder == null)
        {
            wielder = GetComponentInParent<Enemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(wielder.attacking){
           
           edge.GetComponent<Collider>().enabled = true;
       } 
       else{
            edge.GetComponent<Collider>().enabled = false;
       }
    }
}
