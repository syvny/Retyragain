using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using QuestBase;

public class BossQuest : MonoBehaviour
{

    //Quest details
    public string questName;
    public string questDescription;

    //Check if player can accept a quest
    public bool canAccept;
    //Check if quest is active
    public bool isActive = false;

    //Check if player can Complete quest

    public bool canComplete = false;
    //Check if quest is completed
    public bool Completed;

    //if variables to check if quest is completed
    public int requiredAmount = 1;
    public int currentAmount = 0;

    //Quest Predecessor
    public Quest predecessor;

    //Quest Object -- Enemy
    public GameObject questObject;
    public int questID = 4;

    //Reward

    public int rewardExperience = 100;
    public GameObject bossSpawnPosition;

    public int bossID = 11;

    public TextAsset qdList;


    public QuestDataList myQuestDataList = new QuestDataList();




  
    void Start(){
        myQuestDataList = JsonUtility.FromJson<QuestDataList>(qdList.text);
        
        string path = Application.persistentDataPath+"/questProgress.txt";
        
        //get progress from json
        
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            myQuestDataList = JsonUtility.FromJson<QuestDataList>(json);
            
        }else{

            myQuestDataList = JsonUtility.FromJson<QuestDataList>(qdList.text);

            var someString = File.ReadAllText(Application.dataPath + "/Khe Assets/JSON FILES/questProgress.txt");
            
            //no file
            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, someString);
            var check = File.ReadAllText(path);
            Debug.Log("From path: " + check);

        }

        Completed = myQuestDataList.qdL[questID - 1].questDataCompleted;

        
    }

    // Update is called once per frame
    void Update()
    {
    //predecessor is not null, check if player can accept quest
        if(predecessor != null){
            //predecessor is completed
            if(predecessor.Completed){
                //this quest is activated
                if(!isActive){
                    canAccept = true;
                }
                
                
            }
        }
        else{
            //first quest has no predecessor
            if(!isActive){
                    canAccept = true;
                }
            
        }
        if(canComplete){
            canAccept=false;
        }

       

        //quest not completed yet
        if(!Completed){
            if(isActive){

              if(PlayerController.hasKilled == bossID){
                 currentAmount++;
                
    
                 PlayerController.hasKilled = 0;
                   }
            //check Criteria, current amount meets requirement
                 if(currentAmount >= requiredAmount){
                //can Complete quest, return to quest giver
                      canComplete = true;

                     
                //isActive false to allow next quest to be active
                      isActive = false;

                 }
               
            }
        }
        else{

            //quest has been completed
            canAccept=false;
        }
    }

public void acceptQuest(){

    if(canAccept==true){
        PlayerStats.playerOnQuest = true;
        isActive = true;
        canAccept =false;
        spawnBoss();
        
    }
}

public void completeQuest(){
    if(canComplete == true){
        PlayerStats.playerOnQuest = false;
        Completed = true;
        canComplete=false;
        isActive=false;

        QuestData newData = new QuestData();
        newData.questDataID = questID;
        newData.questDataCompleted = Completed;
        saveQuestProgress(myQuestDataList,newData);
        string output = JsonUtility.ToJson(myQuestDataList);
        File.WriteAllText(Application.persistentDataPath + "/questProgress.txt", output);
    }
}


public void spawnBoss(){
    var spawnBoss = (GameObject) Instantiate(questObject,bossSpawnPosition.transform.position,bossSpawnPosition.transform.rotation);
}

public QuestData loadQuestProgress(QuestDataList q){
    QuestData newQuestData = new QuestData();
    newQuestData = q.qdL[questID - 1];

    return newQuestData;


}

public void saveQuestProgress(QuestDataList q, QuestData data){
    q.qdL[data.questDataID - 1].questDataCompleted = data.questDataCompleted;  
}



}
