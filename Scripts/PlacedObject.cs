using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class PlacedObject : MonoBehaviour
{
    public bool Placed {get; private set;}
    private Vector3 origin;   
    public Slider smileySlider;
    public int ID;
    public BoundsInt area;
    public BoundsInt areaCompanions;

    private bool holding = false;
    private float counter = 0f;
    public Item item;
    public Camera cam;

    public float friendsScore;
    public float shadeScore;
    public float friendsRating;
    public float shadeRating;
    public bool waterDeduction;

    private bool replaced;
    public bool dead;


    public bool CanBePlaced(){
        Vector3Int positionInt = PlacementSystem.placementSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(PlacementSystem.placementSystem.CanTakeArea(areaTemp)){
            return true;
        }
        return false;
    }

    public void Place(){
        Vector3Int positionInt = PlacementSystem.placementSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        PlacementSystem.placementSystem.TakeArea(areaTemp);
        PlacementSystem.placementSystem.TakeAreaCompanion(areaTemp, item.tile);
    }

    public void HappinessUpdate(){
        Vector3Int cellPos = PlacementSystem.placementSystem.gridLayout.LocalToCell(transform.position);;
        Vector3 position = PlacementSystem.placementSystem.gridLayout.CellToLocalInterpolated(cellPos);

        Vector3Int positionShade = ShadeSystem.shadeSystem.gridLayout.LocalToCell(position);
        BoundsInt areaShade = area;
        areaShade.position = positionShade;
        int shadeCount = ShadeSystem.shadeSystem.GetShade(areaShade);

        positionShade.x -= 3;
        positionShade.y -= 3;
        BoundsInt areaFriends = areaCompanions;
        areaFriends.position = positionShade;
        int friendsCount = CompanionSystem.companionSystem.GetCompanions(areaFriends, item.companion);

        if(!replaced){
            replaced = true;
            friendsScore = item.getFriendsScore(friendsCount);
            shadeScore = item.getShadeScore(shadeCount);
            smileySlider.value =  shadeScore + friendsScore + 1f;
        }else if(!dead){
            Debug.Log(item.species);
            Timer timer = gameObject.GetComponent<Timer>();
            float progress = timer.getProgress();
            smileySlider.value -= friendsScore + shadeScore;
            friendsRating = item.getFriendsScore(friendsCount);
            shadeRating = item.getShadeScore(shadeCount);
            friendsScore = friendsScore*progress + (1f-progress)*friendsRating;
            shadeScore = shadeScore*progress + (1f-progress)*shadeRating;
            smileySlider.value +=  shadeScore + friendsScore;
        }
    }

    public void DeductHappiness(float minus){
        waterDeduction = true;
        smileySlider.value -= minus;
    }

    public void CheckPlacement(){
        if(!Placed){
            if(CanBePlaced()){
                Place();
                origin = transform.position;
                item.AddAmount(-1);
                StorageView.CloseView();
                StorageView.StartView();
            }else{
                PlacementSystem.storage.Remove(ID);
                Destroy(transform.gameObject);
            }
        }else{
            if(CanBePlaced()){
                Place();
                origin = transform.position;
            }else{
                transform.position = origin;
                Place();
            }
        }
    }

    private void Update(){
        if(!holding && Placed){
            if(Input.GetMouseButton(0)){
                counter += Time.deltaTime;
                if(counter>1.5f){
                    Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    holding = true;
                    if(mousePosition.x<origin.x+1f && mousePosition.x>origin.x-1f && mousePosition.y>origin.y+0.25f && mousePosition.y<origin.y+1.25f){
                        gameObject.AddComponent<PlantDrag>();
                        Vector3Int positionInt = PlacementSystem.placementSystem.gridLayout.WorldToCell(transform.position);
                        BoundsInt areaTemp = area;
                        areaTemp.position = positionInt;
                        PlacementSystem.placementSystem.ClearArea(areaTemp, PlacementSystem.placementSystem.MainTilemap);
                        PlacementSystem.placementSystem.ClearArea(areaTemp, PlacementSystem.placementSystem.CompanionsTilemap);
                    }
                }
            }
        }
        if(holding && Input.GetMouseButtonUp(0)){
            holding = false;
            counter = 0;
        }
    }
}
