using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreView : MonoBehaviour
{
    public static StoreView storeView;
    public Transform BuyView;
    public Transform SellView;
    public GameObject BuyPrefab;
    public GameObject SellPrefab;

    private static Dictionary<GameObject, Item> BuyInstances;
    private static Dictionary<GameObject, Item> SellInstances;


    void Awake(){
        storeView = this;
        BuyInstances = new Dictionary<GameObject, Item>();
        SellInstances = new Dictionary<GameObject, Item>();
    }

    public static void StartView(){
        foreach(KeyValuePair<string, Item> entry in ItemStorage.getItems()){
            Item cur = entry.Value;
            if(cur.level<=LevelSystem.levelSystem.index){
                GameObject newEntry = Instantiate(storeView.BuyPrefab, storeView.BuyView);
                BuyInstances.Add(newEntry, cur);

                if(newEntry.TryGetComponent<BuyItem>(out BuyItem item)){
                    item.InitializeInstance(cur.image, cur.species, cur.count, cur.price);
                }
            }
        }
    }

    public static void CloseView(){
        foreach(KeyValuePair<GameObject, Item> entry in BuyInstances){
            Item cur = entry.Value;
            GameObject newEntry = entry.Key;
            Object.DestroyImmediate(newEntry.gameObject);
        }
        BuyInstances = new Dictionary<GameObject, Item>();
    }



    public static void StartSellView(){
        foreach(KeyValuePair<string, Item> entry in ItemStorage.getItems()){
            Item cur = entry.Value;

            foreach(int price in cur.harvested){
                GameObject newEntry = Instantiate(storeView.SellPrefab, storeView.SellView);
                SellInstances.Add(newEntry, cur);
                if(newEntry.TryGetComponent<SellItem>(out SellItem item)){
                    item.InitializeInstance(cur.stage3, cur.species, price);
                }
            } 
        }
    }

    public static void CloseSellView(){
        foreach(KeyValuePair<GameObject, Item> entry in SellInstances){

            Item cur = entry.Value;
            GameObject newEntry = entry.Key;
            Object.DestroyImmediate(newEntry.gameObject);
        }
        SellInstances = new Dictionary<GameObject, Item>();
    }
}
