using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class GameLeader : MonoBehaviour
{
   public void Leaderboards()
    {
       
        SceneManager.LoadScene("Leaderboards");
        StartCoroutine(Main.Instance.Web.Leaderboard());

    }

}
