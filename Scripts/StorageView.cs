using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    public static StorageView storageView;
    public Transform ScrollView;
    public GameObject StoragePrefab;
    public Camera cam;
    public static Dictionary<GameObject, Item> instances;

    void Awake(){
        instances = new Dictionary<GameObject, Item>();
        storageView = this;
    }
   
    public static void StartView(){
        foreach(KeyValuePair<string, Item> entry in ItemStorage.getItems()){
            Item cur = entry.Value;

            if(cur.count>0){
                GameObject newEntry = Instantiate(storageView.StoragePrefab, storageView.ScrollView);
                instances.Add(newEntry, cur);
                if(newEntry.TryGetComponent<StorageInstance>(out StorageInstance item)){
                    item.InitializeInstance(cur.image, cur.species, cur.count);
                }
            }
        }
    }

    public static void CloseView(){
        foreach(KeyValuePair<GameObject, Item> entry in instances){
            Item cur = entry.Value;
            GameObject newEntry = entry.Key;
            Object.DestroyImmediate(newEntry.gameObject);
        }
        instances = new Dictionary<GameObject, Item>();
    }
    
}
