using CustomQuest;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

/// <summary>
/// Contains a sample player logic to:
/// Move with arrow keys or "WASD".
/// Opening and using the quest wheel.
/// Clicking on a quest giver
/// And picking up an item
/// </summary>
public class CQExamplePlayer : MonoBehaviour
{
    #region Field

    /// <summary>
    /// The dmg this player does
    /// </summary>
    [SerializeField]
    private int damage = 25;

    /// <summary>
    /// The dmg this player does
    /// </summary>
    public int Damage { get { return damage; } set { damage = value; } }






    public float MoveSpeed = 6f;
    float currentVelocity;
    public float smoothRotationTime = 0.25f;
    public bool mobileControls = false;


    float currentSpeed;
    float speedVelocity;

    Transform cameraTransform;



    /// <summary>
    /// Float for resources, use your own resources here.
    /// </summary>
    public float resources;

    /// <summary>
    /// A list of the items this player has picked up
    /// </summary>
    public List<Item> items = new List<Item>();

    /// <summary>
    /// Whether this player is currently attacking or not
    /// </summary>
    public bool attacking;

    /// <summary>
    /// The timer, used for attacking
    /// </summary>
    private float attackTimer;

    private CQPlayerObject cQPlayer;

     public FixedJoystick joystick;
    public Animator animator;


    #endregion Field

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        QuestHandler.StartListening("GiveReward", GetReward);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled() or inactive.
    /// </summary>
    private void OnDisable()
    {
        QuestHandler.StopListening("GiveReward", GetReward);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        cQPlayer = GetComponent<CQPlayerObject>();
        cameraTransform = Camera.main.transform;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        animator.SetBool("Normal Attack (Warrior)", false);
        
        if (attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                 
                attacking = false;
                
               
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            attacking = true;
            attackTimer = 1.2f;
            animator.SetBool("Normal Attack (Warrior)",true);
        }
         else if (Input.GetKey(KeyCode.G))
        {
            attacking = true;
            attackTimer = 10f;
            animator.SetBool("Normal Attack (Warrior)",true);
        }

        

        Movement();
    }

    public void Movement()
    {
       
        Vector2 input = Vector2.zero;

        
        if(mobileControls){
            input = new Vector2(joystick.Horizontal, joystick.Vertical);


        }

        else{

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        
        Vector2 inputDir = input.normalized;

        if(inputDir!=Vector2.zero){
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,rotation, ref currentVelocity, smoothRotationTime);
        }
        float targetSpeed = (MoveSpeed * inputDir.magnitude);
        currentSpeed = Mathf.SmoothDamp(currentSpeed,targetSpeed,ref speedVelocity,0.1f);
        transform.Translate(transform.forward *targetSpeed * Time.deltaTime,Space.World);

         if(inputDir.magnitude>0){

            animator.SetBool("MOVING", true);
            
       
          
   
        }
        else{
            
           animator.SetBool("MOVING", false);
            
        }


      

    }

    /// <summary>
    /// Logic for picking up and item
    /// </summary>
    /// <param name="item">The item picked up by the player</param>
    public void pickUpItem(Item item)
    {
        foreach (SpawnZone zone in item.GetComponent<QuestObject>().criteria.spawnZones)
        {
            if (zone.spawnedObjects.Contains(item.gameObject))
            {
                zone.spawnedObjects.Remove(item.gameObject);
            }
        }
        if (item.GetComponent<QuestObject>().criteria.Quest.activeOptionalCriterias[cQPlayer].Contains(item.GetComponent<QuestObject>().criteria)) //Only picks up an item, if player is currently on the quest
        {
            items.Add(item);
            if (item.GetComponent<QuestObject>().criteria.type == criteriaType.Gather)
            {
                item.GetComponent<QuestObject>().criteria.Progress(cQPlayer, this);
            }
        }

        if (item.GetComponent<QuestObject>().criteria.Quest.activeCriterias[cQPlayer].Contains(item.GetComponent<QuestObject>().criteria)) //Only picks up an item, if player is currently on the quest
        {
            items.Add(item);
            if (item.GetComponent<QuestObject>().criteria.type == criteriaType.Gather)
            {
                item.GetComponent<QuestObject>().criteria.Progress(cQPlayer, this);
            }
        }
        item.gameObject.SetActive(false);
    }

    /// <summary>
    /// An example method for handling rewards
    /// </summary>
    /// <param name="info">Holds the info about the reward</param>
    private void GetReward(EventInfoHolder info)
    {
        switch (info.reward.type)
        {
            case rewardType.Resource:
                this.resources += info.reward.amount;
                break;

            case rewardType.Item:
                for (int i = 0; i < info.reward.amount; i++)
                {
                    //This will just spawn the reward on the ground. If you wish to give it direcly, this is where you add that
                    Instantiate(info.reward.rewardObject, this.transform.position, this.transform.rotation);
                }
                break;
        }
    }
}