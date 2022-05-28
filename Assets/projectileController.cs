using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    // Start is called before the first frame update

    
    public Rigidbody rigidbody;
    public GameObject dustCloud;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(rigidbody.transform.forward * 8, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision other){
        var dc = (GameObject) Instantiate(dustCloud,gameObject.transform.position,gameObject.transform.rotation);

        Destroy(gameObject);
        

    }
}
