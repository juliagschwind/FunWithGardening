using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class Planted : MonoBehaviour
{   
    public Image img;
    public Canvas canvas;
    public string species;
    private int stage = 1;
    public int growthTime;
    public TextMeshProUGUI ProfileTitle;
    public TextMeshProUGUI Trait1;
    public TextMeshProUGUI Trait2;
    public TextMeshProUGUI Trait3;
    public TextMeshProUGUI harvest;

    public GameObject lockedWaterSlider;
    public GameObject lockedWater;

    private bool dead;
    public int waterInterval;
    private float shadeScore;
    private float friendsScore;
    private float temp;


    public void Initialize(Sprite s, Camera cam, string species){
        if(LevelSystem.levelSystem.level > 1){
            lockedWater.SetActive(false);
            lockedWaterSlider.SetActive(false);
        }else{
            lockedWater.SetActive(true);
            lockedWaterSlider.SetActive(true);
        }
        Item item = ItemStorage.storage[species];
        img.sprite = s;
        canvas.worldCamera = cam;
        this.species = species;

        initializeTraits();
        
        this.growthTime = item.growthTime;
        ProfileTitle.text = species;
        this.waterInterval = item.waterInterval;
        int shadeType = item.shadeType;
    }

    public void initializeTraits(){
        Item item = ItemStorage.storage[species];

        Trait1.text = "?";
        Trait2.text = "?";
        Trait3.text = "?";
        int nextTrait = 1;
        for(int i = 0; i<3; i++){
            if(item.unlocked[i]){
                switch(nextTrait){
                    case 1:
                        Trait1.text = item.facts[i];
                        break;
                    case 2:
                        Trait2.text = item.facts[i];
                        break;
                    case 3:
                        Trait3.text = item.facts[i];
                        break;
                }
                nextTrait++;
            }
        }
    }

    public void changeImg(){
        if(stage == 1){
            img.sprite = ItemStorage.storage[species].getStage2();
            stage++;
        }else if(stage == 2){
            if(0 == string.Compare(species, "Tomato")){ //Tomato3 img has size 120x90 instead of 120x60 and therefore needs to be transformed
                img.rectTransform.sizeDelta = new Vector2(3f, 2.25f);
                img.transform.Translate(0.0f, 0.375f, 0.0f);
            }
            img.sprite = ItemStorage.storage[species].getStage3();
            stage++;
        }
    }

    public void Die(){
        harvest.text = "Clean Up";
        dead = true;
        img.sprite = ItemStorage.gameStorage.dead;
        gameObject.GetComponent<PlacedObject>().dead = true;

        if(0 == string.Compare(species, "Tomato")){ //Tomato3 img has size 120x90 instead of 120x60 and therefore needs to be transformed
                img.rectTransform.sizeDelta = new Vector2(3f, 1.5f);
                img.transform.Translate(0.0f, -0.375f, 0.0f);
        }
    }

    public void Harvest(){
        Item item = ItemStorage.storage[species];
        Timer timer = gameObject.GetComponent<Timer>();
        if(!dead){
            double ripeness = timer.ripenessScore;
            PlacedObject temp = gameObject.GetComponent<PlacedObject>();
            float friendsScore = temp.friendsScore;
            float shadeScore = temp.shadeScore;
            float progress = timer.getProgress();
            if(shadeScore >= 0.95f){
                item.Unlock1();
            }
            if(!temp.waterDeduction && LevelSystem.levelSystem.level >=2 && stage >= 2){
                item.Unlock2();
            }
            if(friendsScore >=0.95f && LevelSystem.levelSystem.level >=3){
                item.Unlock3();
            }
            float waterScore = temp.smileySlider.value - shadeScore - friendsScore;
            float happiness = waterScore + (shadeScore-((1f-progress)*temp.shadeRating))*progress + (friendsScore-((1f-progress)*temp.friendsRating))*progress;
            ItemStorage.storage[species].Harvest(Math.Min(ripeness,1.0), happiness); 
        }
        if(gameObject.TryGetComponent<PlacedObject>(out PlacedObject placedObj)){
            Vector3Int positionInt = PlacementSystem.placementSystem.gridLayout.WorldToCell(transform.position);
            BoundsInt areaTemp = placedObj.area;
            areaTemp.position = positionInt;
            PlacementSystem.placementSystem.ClearArea(areaTemp, PlacementSystem.placementSystem.MainTilemap);
            PlacementSystem.placementSystem.ClearArea(areaTemp, PlacementSystem.placementSystem.CompanionsTilemap);
            PlacementSystem.storage.Remove(placedObj.ID);
        }
        Destroy(transform.gameObject);
    }
}
