using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;



public class ItemStorage : MonoBehaviour
{
    public static Dictionary<string,Item> storage;
    public static ItemStorage gameStorage;
    public List<Sprite> images;
    public List<TileBase> tiles;

    public static Dictionary<string, Sprite> stage1;
    public static Dictionary<string, Sprite> stage2;
    public static Dictionary<string, Sprite> stage3;
    
    public static Dictionary<string,Item> harvest;
    public static Dictionary<string, TileBase> tileLookup;

    public Sprite dead;

    public TextMeshProUGUI traitUnlocked;
    public GameObject unlockedNotification;

    void Awake(){
        gameStorage = this;
        stage1 = new Dictionary<string, Sprite>();
        stage2 = new Dictionary<string, Sprite>();
        stage3 = new Dictionary<string, Sprite>();
        harvest = new Dictionary<string, Item>();

        foreach(Sprite img in images){
            if(img.name[img.name.Length-1] == '1'){
                stage1.Add(img.name.Substring(0, img.name.Length-1), img);
            }else if(img.name[img.name.Length-1] == '2'){
                stage2.Add(img.name.Substring(0, img.name.Length-1), img);
            }else if(img.name[img.name.Length-1] == '3'){
                stage3.Add(img.name.Substring(0, img.name.Length-1), img);
            }
        }

        tileLookup = new Dictionary<string, TileBase>();

        foreach(TileBase tile in tiles){
            tileLookup.Add(tile.name, tile);
        }
        storage = new Dictionary<string, Item>();
    }

    public static void StartUp(int timeMode){
        //Real Time = 1, Game Time = 2, Demo Time = 3
        int factor;
        int factor2;

        if(timeMode == 1){
            factor = 86400*30;
            factor2 = 86400;
        }else if(timeMode == 2){
            factor = 86400;
            factor2 = 43200;
        }else{
            factor = 8;
            factor2 = 20;
        }
        storage.Add("Tomato",new Item("Tomato", 0, 1, 4, "Likes sunny areas", "Water once a week", "Is friends with the Carrot",5*factor, 6*factor2, 0, tileLookup["Tomato"], tileLookup["Carrot"], true));
        storage.Add("Radish",new Item("Radish", 0, 1, 2, "Likes cool areas", "Water 2 times a week", "Is friends with the Pumpkin",2*factor, 3*factor2, 2, tileLookup["Radish"], tileLookup["Pumpkin"], true));
        storage.Add("Spinach",new Item("Spinach", 0, 2, 1, "Likes partial shade", "Water 4 times a week", "Is friends with the Strawberries",2*factor, 2*factor2, 1, tileLookup["Spinach"], tileLookup["Strawberry"], true));
        storage.Add("Pumpkin",new Item("Pumpkin", 0, 2, 7, "Likes sunny areas", "Water 2 times a week", "Is friends with the Radish", 4*factor, 3*factor2, 0, tileLookup["Pumpkin"], tileLookup["Radish"], true));
        storage.Add("Strawberry",new Item("Strawberry", 0, 3, 10, "Likes sunny areas", "Water 2 times a week", "Is not friends with the Tomato", 2*factor, 3*factor2, 0, tileLookup["Strawberry"], tileLookup["Tomato"], false));
        storage.Add("Carrot",new Item("Carrot", 0, 3, 5,  "Likes sunny areas", "Water every day", "Is friends with the Radish", 3*factor, 1*factor2, 0, tileLookup["Carrot"], tileLookup["Radish"], true));
        LevelSystem.levelSystem.levelup();
   }


    public void TraitUnlocked(string message){
        this.traitUnlocked.text = message;
        this.unlockedNotification.SetActive(true);
    }

    public static void UpdateCounter (string name, int amount){
        storage[name].AddAmount(amount);
    }

    public static Dictionary<string,Item> getItems(){
        return storage;
    }

    public static Dictionary<string,Sprite> getImages(){
       return stage1;
    }

    public static Dictionary<string,Sprite> getStage1(){
       return stage1;
    }

     public static Dictionary<string,Sprite> getStage2(){
       return stage2;
    }

     public static Dictionary<string,Sprite> getStage3(){
       return stage3;
    }

   public static int GetCount(string name){
        return storage[name].count;
   }

   public static int GetHarvest(string name){
       return storage[name].totalHarvested;
   }

   public static void Harvest(string name, int price){
        storage[name].Sell(price);
   }
}
