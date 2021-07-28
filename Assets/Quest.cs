using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    //Quest details
    public string questName;
    public string questDescription;


    //Check if quest is active
    public bool isActive = false;
    //Check if quest is completed
    public bool Completed;

    //if variables to check if quest is completed
    public int requiredAmount = 1;
    public int currentAmount = 0;

    //Quest Predecessor
    public Quest predecessor;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //predecessor is not null, check if player can accept quest
        if(predecessor != null){
            //predecessor is completed
            if(predecessor.Completed){
                //this quest is activated
                isActive = true;
            }
        }
        else{
            //first quest has no predecessor
            isActive =true;
        }

        //quest not completed yet
        if(!Completed){
            if(isActive){
            //check Criteria, current amount meets requirement
                 if(currentAmount >= requiredAmount){
                //completed quest
                      Completed = true;
                //isActive false to allow next quest to be active
                     isActive = false;

                 }

            }

        }

        

    //Listen to quest needs, kill skeleton

    

        

        //if(completed) then destroy?



    }
}
