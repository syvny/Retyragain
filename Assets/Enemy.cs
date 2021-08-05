using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

   
    
    public Rigidbody rb;
    public Animator animator;

    public Collider col;
    public int damage = 10;

    
    //Dropable Items

    public GameObject[] dropables;
    //Dropable item spawn position
    public GameObject dropPosition;

    //Sight and Attack Ranges

    //Enemy Id
    public int enemyID = 1;
   
   //Player
    public Transform player;
    
    
    
    
    
    public int health = 100;

    public NavMeshAgent agent;
    public float sightRange = 10f;
    public float attackRange  = 5f; 

    public bool attacking = true;
    public float attackTimer;
    
    // Start is called before the first frame update

    void Awake(){
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        

    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("IDLE", true);
        UpdateHealth();


        //Put enemy AI here

        //Check range from player
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance<=sightRange){
            //chase player
            //moving
            Debug.Log("IN SIGHT RANGE");
            animator.SetBool("IDLE", false);
            animator.SetBool("RUNNING",true);
            agent.SetDestination(player.position);

                if(distance<=attackRange){

                    
                    Debug.Log("IN Attack RANGE");
                    //attack timer
                    if(attacking){
                        attackTimer-=Time.deltaTime;
                        if(attackTimer<=0){
                            attacking = false;
                        }

                    }else{
                        //enemy attacks
                        attacking = true;
                        attackTimer = 2f;
                        
                        animator.SetBool("RUNNING",false);
                        animator.SetTrigger("ENEMY ATTACK");
                    }
                    
                }
        }
        else if(distance>sightRange){
            //go back or stop
            animator.SetBool("RUNNING", false);
            animator.SetBool("IDLE", true);
        }


        
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "playerAttack" && PlayerController.attacking == true){
            Debug.Log("Hit");
            //health - playerStats damage
            
            //knock back

            //hit animation
            //knock back
            rb.AddForce(-agent.nextPosition*500f);
            animator.SetTrigger("Get hit");
            health -= PlayerStats.damage;
            Debug.Log(health);
        }



    }

    void UpdateHealth(){

        if(health<=0){
            Die();
  

        }
    }

    void Die(){
        //check if array is empty
        if(dropables[0]!=null){
            //random number generator
            var i = Random.Range(0,10);
            Debug.Log(i);
            if(i<=dropables.Length){
                var dropSpawn = (GameObject) Instantiate(dropables[i],dropPosition.transform.position,dropPosition.transform.rotation);

            }

        }
        PlayerController.hasKilled = enemyID;

        
        col.enabled =false;

        Destroy(gameObject);

    }
}
