using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class Timer : MonoBehaviour
{
    private Planted plant;
    private PlacedObject placedObj;

    public Slider ripenessSlider;
    public Slider waterSlider;

    private DateTime start;
    private DateTime mid;
    private DateTime max;
    private DateTime deathDate;
    private bool dying;
    public double ripenessScore;
    public bool secondStage;
    private float sliderMax;
    private TimeSpan waterInterval;
    private DateTime lastWater;
    private DateTime nextWater;
    private float lastWaterValue;
    private bool waterUnlocked;


    void Start(){
        if(LevelSystem.levelSystem.level > 1){
            waterUnlocked = true;
        }
        this.start = DateTime.Now;
        this.plant = gameObject.GetComponent<Planted>();
        this.placedObj = gameObject.GetComponent<PlacedObject>();
        this.mid = start.Add(new TimeSpan(0,0,0,plant.growthTime/2));
        this.max = start.Add(new TimeSpan(0,0,0,plant.growthTime));
        this.deathDate = start.Add(new TimeSpan(0,0,0,plant.growthTime*2));
        sliderMax = plant.growthTime*2;
        dying = false;


        waterInterval = new TimeSpan(0,0,0,plant.waterInterval);
        waterSlider.value = 0.3f;
        lastWaterValue = 0.3f;
        lastWater = DateTime.Now;
        nextWater = lastWater.Add(2*waterInterval);
    }
    

    public void Water(){
        //if water slider has a value deveating strongly from the ideal water level (smaller than 0.2 or larger than 0.8), 
        //a value will be deducted from the score which influences the price of the harvest
        if(waterUnlocked){
            float waterScore;
            if(waterSlider.value <0.2f){
                waterScore = 0.2f-waterSlider.value;
                placedObj.DeductHappiness(waterScore);
            }
            waterSlider.value += 0.4f;
            lastWaterValue = waterSlider.value;
            lastWater = DateTime.Now;
            nextWater = lastWater.Add(2*waterInterval);

            if(waterSlider.value >0.8f){
                waterScore = waterSlider.value-0.8f;
                placedObj.DeductHappiness(waterScore);
            }
        }
    }

    void Update(){
        //WaterScore
        waterSlider.value = lastWaterValue - (float)((DateTime.Now-lastWater)/(nextWater-lastWater));
        if(waterUnlocked && (waterSlider.value<= 0 || waterSlider.value >=0.99)){
            plant.Die();
            Destroy(this);
        }
        
        //RipenessScore
        ripenessSlider.value = (float)((DateTime.Now-start).TotalSeconds/sliderMax);
        if(DateTime.Now > mid && !dying){
            plant.changeImg();
            if(!secondStage){
                mid = max;
                secondStage = true;
            }else{
                dying = true;
                ripenessScore = 1.3;
            }
        }else if(dying && DateTime.Now>deathDate){
            plant.Die();
            Destroy(this);
        }else if(!dying){
            ripenessScore = (DateTime.Now-start).TotalSeconds/(max-start).TotalSeconds;
        }else if(dying){
            ripenessScore = 1.3-(DateTime.Now-max).TotalSeconds/(deathDate-max).TotalSeconds;
        }
    }

    public float getProgress(){
        return (float)((DateTime.Now-start).TotalSeconds/(sliderMax));
    }
}
