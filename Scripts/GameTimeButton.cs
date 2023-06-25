using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeButton : MonoBehaviour
{
    public GameObject setting;

    public void GameTime(){
        ItemStorage.StartUp(2);
        setting.SetActive(false);
    }

    public void ActionTime(){
        ItemStorage.StartUp(3);
        setting.SetActive(false);
    }

    public void RealTime(){
        ItemStorage.StartUp(1);
        setting.SetActive(false);
    }
}
