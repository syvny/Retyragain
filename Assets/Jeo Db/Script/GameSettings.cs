using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
   public void LoadSetting()
    {
        SceneManager.LoadScene("Settings");
    }
}
