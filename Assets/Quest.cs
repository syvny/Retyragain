using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using QuestBase;



namespace QuestBase{
    [System.Serializable]
    public class QuestData{
        //set quest as completed
        public int questDataID;
        public bool questDataCompleted;
    }

    [System.Serializable]
    public class QuestDataList{
        //set quest as completed
        public QuestData[] qdL;

    }
}
public class Quest : MonoBehaviour
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
    public Enemy questObject;
    public int questID = 1;

    //Reward

    public int rewardExperience = 100;

  
    //randomize quests
    public TextAsset qList;
    //quest progress
    public TextAsset qdList;


    [System.Serializable]
    public class QuestDeetz{
        public string title;
        public string questDetails;
        public string category;
        public int answer;
    }

    [System.Serializable]
    public class QuestList{
        public QuestDeetz[] qL;
    }

    public QuestList myQuestList = new QuestList();

    public string questFileName;

    public QuestDataList myQuestDataList = new QuestDataList();

    public bool isSideQuest = false;


     
   

    // Start is called before the first frame update

  
    void Start()
    {
        
        TextAsset newqList = Resources.Load<TextAsset>(questFileName);
        myQuestList = JsonUtility.FromJson<QuestList>(newqList.text);
        
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
        // myQuestDataList = JsonUtility.FromJson<QuestDataList>(qdList.text);

        
        Completed = myQuestDataList.qdL[questID - 1].questDataCompleted;

        if(isSideQuest == true){
            Completed = false;
            
            QuestDeetz qD = loadQuestDetails(myQuestList); 
            
            questName = qD.title;
            questDescription = qD.questDetails;
            requiredAmount = qD.answer;

            
        }
            
        // QuestData newQuestData = loadQuestProgress(myQuestDataList);
        // Debug.Log(newQuestData.questDataID + " " + newQuestData.questDataCompleted);
        // Completed = newQuestData.questDataCompleted;

        if(!Completed){
            //put load quest here
            QuestDeetz qD = loadQuestDetails(myQuestList); 
            
            questName = qD.title;
            questDescription = qD.questDetails;
            requiredAmount = qD.answer;
            

        }
   
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

                 //Listen to quest needs, kill skeleton

              if(PlayerController.hasKilled == questObject.enemyID){
                 currentAmount++;
                //revert to zero
    
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

public QuestDeetz loadQuestDetails(QuestList q){
    //get random index from questlist array and load the details of the quest to the variables
    int randKey = Random.Range(0,q.qL.Length);
    QuestDeetz newQ = q.qL[randKey];
    
    return newQ;
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
