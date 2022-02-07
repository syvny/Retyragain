using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject mob;
    //enemy to spawn
    
    public int spawnBuffer;
    public float spawnDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Collider col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnBuffer>0){
            if(spawnDelay<=0){
                spawnMob();
                spawnDelay = 5f;
                spawnBuffer--;
            }
            spawnDelay-=Time.deltaTime;
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
    }   

    public void spawnMob(){
        Vector3 sp = RandomPointInBounds(GetComponent<Collider>().bounds);
        
        var spawning = (GameObject) Instantiate(mob,sp,gameObject.transform.rotation);
    }

    public void initiateSpawn(){
        spawnBuffer++;
    }
}
