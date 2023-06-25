using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SellItem : MonoBehaviour
{
    public TextMeshProUGUI species;
    public TextMeshProUGUI price;
    public Image img;    
    public int priceInt;

    public void Sell(){
        int price = int.Parse(this.price.text);
        int money = Coins.GetCoins();
        
        Coins.AddCoins(price);
        ItemStorage.Harvest(this.species.text, priceInt);
        StoreView.CloseSellView();
        StoreView.StartSellView();
    }

     public void InitializeInstance(Sprite sprite, string name, int price){
        img.sprite = sprite; 
        this.species.text = name;
        this.price.text = price.ToString();
        priceInt = price;
        if(0 == string.Compare(name, "Tomato")){
            img.rectTransform.sizeDelta = new Vector2(190f, 100f);
            img.transform.Translate(0.0f, 30f, 0.0f);
        }
    }
}

