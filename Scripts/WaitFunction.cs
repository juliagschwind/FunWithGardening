using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFunction : MonoBehaviour
{
    private float current;
    private float goal;
    private Watering water;

    public void Initialize(float seconds, Watering water){
        current = 0;
        goal = seconds;
        this.water = water;
    }

    void Update(){
        if(current<goal){
            current += Time.deltaTime;
        }else{
            water.NextStage();
            Destroy(this);
        }
    }
}
