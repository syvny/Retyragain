using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update

    
    public Rigidbody rigidbody;
    public PlayerStats playerStats;
    public GameObject dustCloud;
    //enemy projectiles have different types varying damage
    
    public int damage = 50;
    
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(rigidbody.transform.forward * 4);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            playerStats.takeDamage(damage);
        }
        
        var dc = (GameObject) Instantiate(dustCloud,gameObject.transform.position,gameObject.transform.rotation);

        Destroy(gameObject);

    }
}
