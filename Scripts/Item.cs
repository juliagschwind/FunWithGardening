using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Tilemaps;



public class Item
{
    public string species;
    public int count;
    public int level;
    public int price;
    public int maxSell;
    public Sprite image;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    public string[] facts;
    public bool[] unlocked; //indicates which traits have been unlocked
    public int growthTime;
    public List<int> harvested;
    public int totalHarvested;
    public int waterInterval;
    public int shadeType; //0 = sunny, 1 = partial shade, 2 = full shade
    public TileBase tile;
    public bool friends; //indicates if the companion is a friend or not
    public TileBase companion;
    private int[] shadeRange;

    public Item(string species, int count, int level, int price, string fact1, string fact2, string fact3, int growthTime, int waterInterval, int shadeType, TileBase tile, TileBase companion, bool friends){
        unlocked = new bool[3];
        if(level == 1){
            unlocked[0] = true;
        }else if(level == 2){
            unlocked[1] = true;
        }else{
            unlocked[2] = true;
        }

        facts = new string[3];
        facts[0] = fact1;
        facts[1] = fact2;
        facts[2] = fact3;
        
        this.species = species;
        this.count = count;
        this.level = level;
        this.price = price;
        this.harvested = new List<int>();
        this.image = ItemStorage.getImages()[species];
        this.stage1 = ItemStorage.getStage1()[species];
        this.stage2 = ItemStorage.getStage2()[species];
        this.stage3 = ItemStorage.getStage3()[species];
        this.growthTime = growthTime;
        this.maxSell = 5*price;
        this.totalHarvested = 0;
        this.waterInterval = waterInterval;
        this.shadeType = shadeType;
        this.tile = tile;
        this.friends = friends;
        this.companion = companion;


        if(shadeType == 0){
            shadeRange = new int[] {0,2};
        }else if(shadeType == 1){
            shadeRange = new int[] {3,6};
        }else if(shadeType == 2){
            shadeRange = new int[] {7,9};
        }
    }
    
    public float getShadeScore(int shadeCount){
        float shadeScore;
        if(shadeCount >= shadeRange[0] && shadeCount <= shadeRange[1]){
            shadeScore = 1f;
        }else if(shadeCount >= shadeRange[0]-2 && shadeCount <= shadeRange[1]+2){
            shadeScore = 0.5f;
        }else{
            shadeScore = 0.0f;
        }
        return shadeScore;
    }

    public float getFriendsScore(int friendsCount){
        float friendsScore;
        if(ItemStorage.storage[species].friends){
            if(friendsCount>=9){
                friendsScore = 1f;
            }else if(friendsCount>=4){
                friendsScore = 0.5f;
            }else{
                friendsScore = 0f;
            }
        }else{
            if(friendsCount>=9){
                friendsScore = 0f;
            }else if(friendsCount>=4){
                friendsScore = 0.5f;
            }else{
                friendsScore = 1f;
            }
        }
        return friendsScore;
    }

    public void AddAmount(int amount){
        count += amount;
    }

    public Sprite getStage1(){
       return stage1;
    }

    public Sprite getStage2(){
       return stage2;
    }

    public Sprite getStage3(){
       return stage3;
    }

    public void Harvest(double ripeness, double happiness){
        totalHarvested++;
        harvested.Add((int)Math.Ceiling(ripeness*0.4*maxSell+ happiness/3.0*0.6*maxSell));
        LevelSystem.levelSystem.Update1(totalHarvested, species);
        LevelSystem.levelSystem.Update2((int)Math.Ceiling((happiness)/3.0*10), species); 
    }

    public int GetHarvest(){
        return totalHarvested;
    }

    public void Sell(int price){
        harvested.Remove(price);
    }

    public void Unlock1(){
        if(!unlocked[0]){
            unlocked[0] = true;
            string message = species + " Trait Unlocked:\n" + facts[0];
            ItemStorage.gameStorage.TraitUnlocked(message);
        }
    }

    public void Unlock2(){
        if(!unlocked[1]){
            unlocked[1] = true;
            string message = species + " Trait Unlocked:\n" + facts[1];
            ItemStorage.gameStorage.TraitUnlocked(message);
        }
    }

    public void Unlock3(){
        if(!unlocked[2]){
            unlocked[2] = true;
            string message = species + " Trait Unlocked:\n" + facts[2];
            ItemStorage.gameStorage.TraitUnlocked(message);
        }
    }
}
