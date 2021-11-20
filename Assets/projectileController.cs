using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    // Start is called before the first frame update

    
    public Rigidbody rigidbody;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(rigidbody.transform.forward * 4);
    }

    void OnCollisionEnter(Collision other){
        Destroy(gameObject);

    }
}
