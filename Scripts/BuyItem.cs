using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public TextMeshProUGUI species;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI price;
    public Image img;
    
    public void Buy(){
        int price = int.Parse(this.price.text);
        int money = Coins.GetCoins();
        
        if(money>=price&&(ItemStorage.GetCount(this.species.text)<9)){
            Coins.AddCoins((-1)*price);
            ItemStorage.UpdateCounter(this.species.text, 1);
            counter.text = ItemStorage.GetCount(this.species.text).ToString();
        }else if(ItemStorage.GetCount(this.species.text)>8){
            Debug.Log("Reached Max Number of Seeds");
        }else{
            Debug.Log("Not Enough Coins");
        }
    }

     public void InitializeInstance(Sprite sprite, string name, int count, int price){
        img.sprite = sprite; 
        this.counter.text = count.ToString();
        this.species.text = name;
        this.price.text = price.ToString();
    }
}
