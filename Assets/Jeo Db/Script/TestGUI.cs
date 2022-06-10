using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGUI : MonoBehaviour {

    private CharacterClass class1 = new MageClass();
    private CharacterClass class2 = new WarriorClass();
    public Button StartBtn;
    public Button WarriorBtn;
    public Button MageBtn;
    public string HeroClass = "";

    // Start is called before the first frame update
    void Start()
    {
        WarriorBtn.onClick.AddListener(WarriorClass);
        MageBtn.onClick.AddListener(MageClass);
        StartBtn.onClick.AddListener(() => {
            if (HeroClass != ""){ 
                    StartCoroutine(Main.Instance.Web.UpdateHeroClass(HeroClass));
                }
            });
        
    }

    public void WarriorClass(){
         HeroClass = "Warrior";
    }

    public void MageClass(){
         HeroClass = "Mage";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        //GUILayout.Label(class1.characterClassName);
        //GUILayout.Label(class1.characterClassDesc);
        //GUILayout.Label(class2.characterClassName);
        //GUILayout.Label(class2.characterClassDesc);

    }
}
