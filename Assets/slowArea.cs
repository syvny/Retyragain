using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowArea : MonoBehaviour
{

    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerController.MoveSpeed-=3f;
            Debug.Log("Slowed");
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag== "Player"){
            playerController.MoveSpeed+=3f;
        }
    }
}
