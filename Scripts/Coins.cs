using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Coins : MonoBehaviour
{
    public static Coins game;
 
    public static TextMeshProUGUI text;
    public static int amount = 20;

    void Awake(){   
        game = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    public static int GetCoins(){
        return amount;
    }

    public static void AddCoins (int coins){
        amount += coins;
        text.text = amount.ToString();
        LevelSystem.levelSystem.Update3(amount);
    }
}
