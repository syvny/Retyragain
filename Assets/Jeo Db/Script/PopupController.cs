using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupController : MonoBehaviour
{
   public void Cancel()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
