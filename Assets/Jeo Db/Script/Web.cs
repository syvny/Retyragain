using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using progManager;
using QuestBase;

public class Web : MonoBehaviour
{
    ArrayList strings = new ArrayList();
    public GameObject userInfoContainer;
    public GameObject userInfoTemplate;
    
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate("http://localhost/Reschool/GetDate.php"));
        //StartCoroutine(GetUsers("http://localhost/Reschool/GetUsers.php"));
        //StartCoroutine(Login("jeosolima", "12345"));
        //StartCoroutine(RegisterUser("jeosolima", "12345"));
        StartCoroutine(Leaderboard());
        //StartCoroutine(UpdateHeroClass());

    }

    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "Login Success.")
            {
                Debug.Log(www.downloadHandler.text);  
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);

                Debug.Log("USERNAME: " + PlayerPrefs.GetString("username") + "PASSWORD: " + PlayerPrefs.GetString("password"));
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("HomeScreen");


            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                wrongInputPanel.hasWrongPanel =true;
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "New record created successfully")
            {
                Debug.Log(www.downloadHandler.text);
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetString("level", "1");

                StartCoroutine(Main.Instance.Web.createFreshSaves());

                
                statsHome.isNewPlayer = true;

                SceneManager.LoadScene("Class");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                registerExceptions.registerException = true;
                
            }
        }
    }

    public IEnumerator Leaderboard()
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/Reschool/Leaderboard.php"))
        {

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string rawResponse = www.downloadHandler.text;

                string[] users = rawResponse.Split('*');

                for(int i = 0; i < users.Length; i++){
                    // Debug.Log("Current data:" + users[i]);
                    if(users[i] != ""){
                        string[] userInfo = users[i].Split(',');
                        Debug.Log("Username:" + userInfo[0] +  "Level:" + userInfo[1] + "Experience:" + userInfo[2]);
                        GameObject userdata = (GameObject)Instantiate(userInfoTemplate);
                        userdata.transform.SetParent(userInfoContainer.transform);
                        userdata.GetComponent<UserInfo>().UsernameTitle.text = userInfo[0];
                        userdata.GetComponent<UserInfo>().LevelTitle.text = userInfo[1];
                        userdata.GetComponent<UserInfo>().ExpTitle.text = userInfo[2];
                    }
                }
            }
   
           
        }
       
    }

    public IEnumerator UpdateHeroClass(string HeroClass)
    {
        WWWForm classHero = new WWWForm();
        classHero.AddField("heroClass", HeroClass);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/UpdateHeroClass.php", classHero))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                
                
            }
            else
            {
                PlayerPrefs.SetString("class",HeroClass);
                Debug.Log(www.downloadHandler.text);
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Main");
                
            }
        }
    }



    //KHE
    public IEnumerator sendSaves(){

        string savePath =  Application.persistentDataPath + "/saveFile.txt";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Open);

        PlayerData loadedPlayerData = formatter.Deserialize(stream) as PlayerData;

        var saveString = JsonUtility.ToJson(loadedPlayerData);
        

        stream.Close();

        string questPath = Application.persistentDataPath + "/questProgress.txt";

        var questString = File.ReadAllText(questPath);



        string testPass = PlayerPrefs.GetString("password");
        string testUser = PlayerPrefs.GetString("username");

        WWWForm Saves = new WWWForm();
        Saves.AddField("saveFile", saveString);
        Saves.AddField("questFile", questString);
        Saves.AddField("username", testUser);
        Saves.AddField("password", testPass);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/sendSaves.php", Saves))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "Success")
            {
                Debug.Log(www.downloadHandler.text);  
                yield return new WaitForSeconds(3);

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }

    public IEnumerator retrieveSaveFile(){

        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        WWWForm Saves = new WWWForm();
        Saves.AddField("username", username);
        Saves.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/retrieveSaveFile.php", Saves))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text != null)
            {
                Debug.Log(www.downloadHandler.text);  


                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.persistentDataPath + "/saveFile.txt";
                FileStream stream = new FileStream(path, FileMode.Create);

                if(File.Exists(path)){
                    //overwrite/create on path
                    PlayerData playerData = new PlayerData();
                    playerData = JsonUtility.FromJson<PlayerData>(www.downloadHandler.text);

                    formatter.Serialize(stream, playerData);
                    stream.Close();
                }
                else{
                    Debug.Log("File not found");
                }
               
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }


    }

    public IEnumerator retrieveQuestFile(){
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        WWWForm Saves = new WWWForm();
        Saves.AddField("username", username);
        Saves.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/retrieveQuestFile.php", Saves))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text != null)
            {
                Debug.Log("QUEST FILE "+ www.downloadHandler.text);  

                string path = Application.persistentDataPath+"/questProgress.txt";

                if(File.Exists(path)){
                    //Overwrite
                    File.WriteAllText(path, www.downloadHandler.text);
                }
                else{

                    Debug.Log("No Quest FIle Found");
                }
               
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }

    public IEnumerator createFreshSaves(){
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        WWWForm Saves = new WWWForm();
        Saves.AddField("username", username);
        Saves.AddField("password", password);

        //Create questProgress.txt and saveFile.txt on persistentDataPath

        //Quest Progress

        string questPath = Application.persistentDataPath + "/questProgress.txt";

        var someString = File.ReadAllText(Application.dataPath + "/Khe Assets/JSON FILES/questProgress.txt");
            
        FileStream stream = File.Create(questPath);
        stream.Close();
        File.WriteAllText(questPath, someString);

        //Save File

        string path = Application.persistentDataPath + "/saveFile.txt";
        stream = new FileStream(path, FileMode.Create);
        
        PlayerData playerData = new PlayerData();

        playerData.playerHealth = 100;
        playerData.playerMaxHealth = 100;
        playerData.playerMana = 100;
        playerData.playerMaxMana = 100;
        playerData.playerLevel = 1;
        playerData.playerExperience = 0;
        playerData.playerHpPotions = 0;
        playerData.playerMpPotions = 0;

        stream.Close();

        //Send to db

        string saveString = JsonUtility.ToJson(playerData);
        var questString = File.ReadAllText(questPath);
        string testPass = PlayerPrefs.GetString("password");
        string testUser = PlayerPrefs.GetString("username");
        string lev = PlayerPrefs.GetString("level");
        string exp = PlayerPrefs.GetString("experience");

        Debug.Log(saveString);
        Debug.Log(questString);

        WWWForm newSaves = new WWWForm();
        newSaves.AddField("saveFile", saveString);
        newSaves.AddField("questFile", questString);
        newSaves.AddField("username", testUser);
        newSaves.AddField("password", testPass);
        newSaves.AddField("level", lev);
        newSaves.AddField("experience", exp);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/sendSaves.php", newSaves))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "Success")
            {
                Debug.Log(www.downloadHandler.text);  
            
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }

    public IEnumerator getClass(){
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        WWWForm classForm = new WWWForm();
        classForm.AddField("username", username);
        classForm.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/getClass.php", classForm))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text != null)
            {
                Debug.Log(www.downloadHandler.text);  

                PlayerPrefs.SetString("class", www.downloadHandler.text);
               
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }

    }

    public IEnumerator getLevel(){
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        WWWForm levelForm = new WWWForm();
        levelForm.AddField("username", username);
        levelForm.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/getLevel.php", levelForm))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text != null)
            {
                Debug.Log(www.downloadHandler.text);  

                PlayerPrefs.SetString("level", www.downloadHandler.text);
               
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }

    public IEnumerator setLevelAndExp(){
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");
        int level = PlayerPrefs.GetInt("trueLevel");
        int exp = PlayerPrefs.GetInt("experience");

        WWWForm lneForm = new WWWForm();
        lneForm.AddField("username", username);
        lneForm.AddField("password", password);
        lneForm.AddField("level", level);
        lneForm.AddField("experience", exp);

        Debug.Log("EXP: " + exp + "LEVEL: " + level);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Reschool/setLevelAndExp.php", lneForm))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "Success")
            {
                Debug.Log(www.downloadHandler.text);  

                
               
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //SceneManager.LoadScene("HomeScreen");
                 //StartCoroutine(Leaderboard());
            }
        }
    }
    
}
