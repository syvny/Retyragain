using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField password1;
    public Button SubmitBtn;
    // Start is called before the first frame update
    void Start()
    {
        SubmitBtn.onClick.AddListener(() => {
            if ((username.text != "" && password.text != "") && password1.text != ""){
                    if (password.text == password1.text){
                        StartCoroutine(Main.Instance.Web.RegisterUser(username.text, password.text));
                    }
                    else{
                        Debug.Log("Password doesnt match");
                    }
            }
     
            });
    }

}