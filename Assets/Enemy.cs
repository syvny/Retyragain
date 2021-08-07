using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

   
    
    public Rigidbody rb;
    public Animator animator;

    public Collider col;
    public int damage = 20;

    
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

   //sword, activate collider in animation event

   public GameObject sword;
    
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
        agent.enabled = true;
        
        
        animator.SetBool("IDLE", true);
        UpdateHealth();


        //Put enemy AI here

        //Check range from player
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance<=sightRange){
            //chase player
            //moving
            chasePlayer();
            

                if(distance<=attackRange){

                    facePlayer();
      
                    attackPlayer();
                    
                }
        }
        else if(distance>sightRange){
            //go back or stop
            //disable navmesh agent to stop set destination
            agent.enabled =false;
            animator.SetBool("RUNNING", false);
            animator.SetBool("IDLE", true);
        }


        
        
    }

    void chasePlayer(){
       
            animator.SetBool("IDLE", false);
            animator.SetBool("RUNNING",true);
            agent.SetDestination(player.position);

    }

    void attackPlayer(){
        //set collider of sword to active
        
        animator.SetBool("RUNNING",false);
        animator.SetTrigger("ENEMY ATTACK");
    }

    void facePlayer(){
        Vector3 direction =  (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = lookRotation;
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "playerAttack" && PlayerController.attacking == true){
            Debug.Log("Hit");
          
           
            animator.SetTrigger("Get hit");
            health -= PlayerStats.damage;
           
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
        //check if player is on quest before changing has killed
        if(PlayerStats.playerOnQuest){
             PlayerController.hasKilled = enemyID;

        }
        else{
            PlayerController.hasKilled = 0;
        }

        
        col.enabled =false;

        Destroy(gameObject);

    }

    void activateSword(){
        sword.GetComponent<Collider>().enabled = true;
    }

    void deactivateSword(){
        sword.GetComponent<Collider>().enabled = false;
    }
}
