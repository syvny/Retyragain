using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class questGiver : MonoBehaviour
{
    // Start is called before the first frame update

    public Quest quest;
    public Transform player;
    public TextMesh overlay;

    //Quest Panel
    public GameObject questPanel;
    public TMP_Text questTitle;
    public TMP_Text questDescription;

    public Button submitAnswer;
    public Button closeQuestPanel;

    public TMP_InputField input;


    //Reward Panel
    public GameObject rewardPanel;

    public TMP_Text rewardDetails;
    public Button closeRewardPanel;

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
        else if(quest.canAccept){
            overlay.text = "!";
        }
        else {
            overlay.text = ""; 
        }
        
        canInteract = checkInteract();
        if(canInteract){

            if(quest.canAccept){
                if(Input.GetKeyDown("space")){
                //trigger quest dialogue but for now activate quest
                    showQuestPanel();
                    
                      submitAnswer.onClick.AddListener(answerQuestion);
                       
                }

            }
            else{
                    //other dialougue
                    //Debug.Log("Cant accept quest yet");
            }
            if(quest.canComplete){
                if(Input.GetKeyDown("space")){
                //trigger quest dialogue but for now complete quest
                    
                        quest.completeQuest();
                        rewardPlayer();
                         overlay.text = "";
                        
                       
                }
            }
            else{
                //other dialougue
                // Debug.Log("Player cant complete quest yet");

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


    void showQuestPanel(){

        questTitle.text = quest.questName;
        questDescription.text = quest.questDescription;

        questPanel.SetActive(true);

        closeQuestPanel.onClick.AddListener(closePanelQuest);
    }

    void hideQuestPanel(){
        questTitle.text = quest.questName;
        questDescription.text = quest.questDescription;

        questPanel.SetActive(false);
    }
     public void answerQuestion(){
        //button
        int playerAnswer = int.Parse(input.text);
        Debug.Log(playerAnswer);
        if(playerAnswer==quest.requiredAmount){
            //correct answer so player can accept the quest
            quest.acceptQuest();
             overlay.text = "";
                        
            Debug.Log("Pleyer accepted quest");
            questPanel.SetActive(false);
            
        }
        else{
            //wrong answer
            Debug.Log("Wrong Answer");
           
        }

    }

    public void rewardPlayer(){
        rewardPanel.SetActive(true);
        rewardDetails.text = "You earned " + quest.rewardExperience + " experience points!";
        PlayerStats.experience += quest.rewardExperience;
        closeRewardPanel.onClick.AddListener(closePanelReward);
    }

    void closePanelReward(){
        rewardPanel.SetActive(false);
    }
    
    void closePanelQuest(){
        questPanel.SetActive(false);
    }
}
