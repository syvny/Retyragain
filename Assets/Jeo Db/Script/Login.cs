using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Button LoginBtn;

    // Start is called before the first frame update
    void Start()
    {
        LoginBtn.onClick.AddListener(() => {
            if(username.text != "" || password.text != ""){
            StartCoroutine(Main.Instance.Web.Login(username.text, password.text));
            }
            });
    }

}
