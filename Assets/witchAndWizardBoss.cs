using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class witchAndWizardBoss : MonoBehaviour
{


    //Boss is basically enemy but with wait time and is slower
    //Different Animations
    public Rigidbody rb;
    public Animator animator;

    
    public int damage = 80;

    //Sight and Attack Ranges

    //Enemy Id
    public int enemyID = 15;
   
   //Player
    public Transform player;
    
    
    
    
    int maxhealth = 500;
    public int health = 500;

    bool hasSummoned = false;

    public NavMeshAgent agent;
    public float sightRange = 10f;
    public float attackRange  = 8f; 

   //sword, activate collider in animation event

   public GameObject bullet;
   public GameObject projectilePosition;


   public float waitTime; //wait time

   public GameObject summonPosition1;
   public GameObject summonPosition2;

   public GameObject summon;

    
    // Start is called before the first frame update

    void Awake(){
       

    }
    void Start()
    {
        
         player = GameObject.FindWithTag("Player").transform;
        
        
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
            
    
                    facePlayer();
      
  
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
     
           animator.SetBool("IDLE", false);
           animator.SetTrigger("ENEMY ATTACK");
           

    }

    void wait(){

        //animation event ot make boss wait
        agent.enabled=false;
        waitTime=3f;
    }

    void facePlayer(){
        Vector3 direction =  (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = lookRotation;

        attackPlayer();
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

    void launchProjectile(){
        var proj =  (GameObject )Instantiate(bullet,projectilePosition.transform.position,projectilePosition.transform.rotation);
    }

    void summonMob(){
        var summ1 = (GameObject )Instantiate(summon,summonPosition1.transform.position,summonPosition1.transform.rotation);
        var summ2 = (GameObject )Instantiate(summon,summonPosition2.transform.position,summonPosition2.transform.rotation);
    }
   
}
