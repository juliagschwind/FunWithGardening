using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Image unlocked1;
    public TextMeshProUGUI unlocked1Name;
    public GameObject levelUpScreen;
    public GameObject level1Screen;
    public GameObject level2Screen;
    public GameObject level3Screen;
    
    public void CloseView(){
        if(0 == string.Compare(LevelSystem.levelSystem.unlocked1Name.text, "Tomato")){
            this.unlocked1.rectTransform.sizeDelta = new Vector2(190f, 65f);
            this.unlocked1.transform.Translate(0.0f, -35f, 0.0f);
        }
        this.levelUpScreen.SetActive(false);
        if(LevelSystem.levelSystem.level == 1){
            level1Screen.SetActive(true);
        }else if(LevelSystem.levelSystem.level == 2){
            level2Screen.SetActive(true);
        }else if(LevelSystem.levelSystem.level == 3){
            level3Screen.SetActive(true);
        }
    }

    public void CloseLevel1(){
        level1Screen.SetActive(false);
    }

    public void CloseLevel2(){
        level2Screen.SetActive(false);
    }

    public void CloseLevel3(){
        level3Screen.SetActive(false);
    }
}
