using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mummyBoss : MonoBehaviour
{


    //Boss is basically enemy but with wait time and is slower
    //Different Animations
    public Rigidbody rb;
    public Animator animator;

    
    

    //Sight and Attack Ranges

    //Enemy Id
    public int enemyID = 41;
   
   //Player
    public Transform player;

    public int damage = 60;

    public GameObject summon;

    public GameObject summonPosition1;
    public GameObject summonPosition2;
    
    
    
    
    
    public int health = 700;
    int maxhealth= 700;

    public NavMeshAgent agent;
    public float sightRange = 10f;
    public float attackRange  = 5f; 

   //sword, activate collider in animation event

   public GameObject sword;
   bool hasSummoned =false;


   public float waitTime; //wait time
    
    // Start is called before the first frame update

    void Awake(){
       

    }
    void Start()
    {
        
         player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(PlayerController.isDead){
            animator.SetBool("IDLE", true);

        }
       else{
           agent.enabled = true;
        
        
        animator.SetBool("IDLE", true);
        UpdateHealth();


        //Put enemy AI here

        if(waitTime<=0){
            //boss done waiting ang can attack
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
            
            animator.SetBool("RUNNING", false);
            animator.SetBool("IDLE", true);

            agent.enabled=false;
        }
        
        }
        else{
            agent.enabled=false;
        }
        waitTime-=Time.deltaTime;
      
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

    void wait(){

        //animation event ot make boss wait
        agent.enabled=false;
        waitTime=7f;
    }

    void facePlayer(){
        Vector3 direction =  (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = lookRotation;
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "playerAttack" && PlayerController.attacking == true){
            Debug.Log("Hit");
          
           
            
            health -= PlayerStats.damage;
           
        }



    }

     void OnTriggerEnter(Collider other){

         //trigger is the special attack
        if(other.tag == "playerAttack" && PlayerController.attacking == true){
            Debug.Log("Hit");
          
           
            animator.SetTrigger("Get hit");
            //double damage
            health -= PlayerStats.damage * 2;
           
        }



    }

    void UpdateHealth(){

        if(health<=0){
            Die();
  

        }
        if(health<=maxhealth/2 && hasSummoned == false){
            animator.SetTrigger("SUMMON");
            hasSummoned = true;
        }
    }

    void Die(){
     
        //check if player is on quest before changing has killed
        if(PlayerStats.playerOnQuest){
             PlayerController.hasKilled = enemyID;

        }
        else{
            PlayerController.hasKilled = 0;
        }

        
        

        Destroy(gameObject);

    }

    void activateSword(){
        sword.GetComponent<Collider>().enabled = true;
    }

    void deactivateSword(){
        sword.GetComponent<Collider>().enabled = false;
    }

    void summonMob(){
        var summ1 = (GameObject )Instantiate(summon,summonPosition1.transform.position,summonPosition1.transform.rotation);
        var summ2 = (GameObject )Instantiate(summon,summonPosition2.transform.position,summonPosition2.transform.rotation);
    }
}
