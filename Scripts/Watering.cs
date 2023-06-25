using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    public GameObject water;
    public GameObject can;
    public GameObject plant;
    private Timer timer;
    private int stage;

    void Awake(){
        this.timer = plant.GetComponent<Timer>();
    }

    public void Water(){
        if(LevelSystem.levelSystem.level>=2){
            stage = 1;
            can.SetActive(true);
            WaitFunction animationTimer = gameObject.AddComponent<WaitFunction>();
            animationTimer.Initialize(0.5f, this);
        }
    }

    public void NextStage(){
        switch(stage){
            case 1:
                Tilt();
                break;
            case 2:
                TiltBack();
                break;
            case 3:
                EndWater();
                break;
            default:
                break;
        }
    }

    public void Tilt(){
        timer.Water();
        water.SetActive(true);
        can.GetComponent<RectTransform>().Rotate(new Vector3(0,0,45));
        stage++;
        WaitFunction animationTimer = gameObject.AddComponent<WaitFunction>();
        animationTimer.Initialize(2f, this);
    }

    public void TiltBack(){
        water.SetActive(false);
        can.GetComponent<RectTransform>().Rotate(new Vector3(0,0,315));
        stage++;
        WaitFunction animationTimer = gameObject.AddComponent<WaitFunction>();
        animationTimer.Initialize(1f, this);
    }

    public void EndWater(){
        can.SetActive(false);
        stage = 0;
    }
}
