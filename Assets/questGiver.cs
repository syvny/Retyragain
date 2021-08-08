using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questGiver : MonoBehaviour
{
    // Start is called before the first frame update

    public Quest quest;
    public Transform player;
    public TextMesh overlay;

    public float interactRange = 3f;
    public bool canInteract;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(quest.canComplete){
            overlay.text = "!";
            overlay.color = Color.green;
        }
        
        canInteract = checkInteract();
        if(canInteract){

            if(quest.canAccept){
                if(Input.GetKeyDown("space")){
                //trigger quest dialogue but for now activate quest
                    
                        quest.acceptQuest();
                         overlay.text = "";
                        
                        Debug.Log("Pleyer accepted quest");
                }

            }
            else{
                    //other dialougue
                    Debug.Log("Cant accept quest yet");
            }
            if(quest.canComplete){
                if(Input.GetKeyDown("space")){
                //trigger quest dialogue but for now activate quest
                    
                        quest.completeQuest();
                         overlay.text = "";
                        
                       
                }
            }
            else{
                //other dialougue
                Debug.Log("Player cant complete quest yet");

            }
           
        }
    }


    bool checkInteract(){
        
         float distance = Vector3.Distance(transform.position, player.position);
         if(distance<=interactRange){
             return true;
         }
        else{
            return false;
        }
         
    }

 
    
}
