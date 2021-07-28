using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnCollision : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mob;
    public GameObject spawnPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
        
            var newSpawn = (GameObject)Instantiate(mob,spawnPosition.transform.position,spawnPosition.transform.rotation);
        }
    }
}
