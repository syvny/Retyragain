using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rangedEnemy : MonoBehaviour
{

   
    
    public Rigidbody rb;
    public Animator animator;

    
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
    

   //sword, activate collider in animation event
   // in this case launching a projectile

   public GameObject projectile;
   public GameObject projectilePosition;


   public ParticleSystem deathEffect;

   public bool isSlowed = false;
   public float slowDuration;


    
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
        if(isSlowed){
            agent.enabled=false;
            slowDuration-=Time.deltaTime;
            //slow attack stops enemy for 5 seconds
        }
        if(slowDuration<=0){
            isSlowed = false;
            agent.enabled=true;
        }
        if(PlayerController.isDead){
            animator.SetBool("IDLE", true);

        }
       else{
           agent.enabled = true;
        
        
        animator.SetBool("IDLE", true);
        UpdateHealth();


        //Put enemy AI here

        //Check range from player
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance<=sightRange){
            //ranged enemies dont move and only attack once they see player

                    facePlayer();
      
                    
                    
                
        }
        else if(distance>sightRange){
            //go back or stop
            //disable navmesh agent to stop set destination
            
            
            animator.SetBool("IDLE", true);
        }

       }
        

        
        
    }

  

    void attackPlayer(){
        //set collider of sword to active
        
        animator.SetBool("IDLE",false);
        animator.SetTrigger("ENEMY ATTACK");
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
          
           
            animator.SetTrigger("Get hit");
            health -= PlayerStats.damage;
           
        }

        if(other.gameObject.tag == "Snowball"){
            Debug.Log("Slowed");
            
            isSlowed=true;
            slowDuration = 5f;

            animator.SetTrigger("Get hit");
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

        
        

        Destroy(gameObject);


    }

    void launchProjectile(){

        var proj =  (GameObject )Instantiate(projectile,projectilePosition.transform.position,projectilePosition.transform.rotation);
    }
}
