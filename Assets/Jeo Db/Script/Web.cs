using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

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
                SceneManager.LoadScene("HomeScreen");


            }
            else
            {
                Debug.Log(www.downloadHandler.text);
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
                SceneManager.LoadScene("Class");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                
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
                Debug.Log(www.downloadHandler.text);
                SceneManager.LoadScene("Main");
                
            }
        }
    }

    
}
