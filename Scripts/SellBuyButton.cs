using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBuyButton : MonoBehaviour
{
    public GameObject sell;
    public GameObject buy;

    public static bool sellOpen = false;
    
    public void SwitchSell(){
        if(!sellOpen){
            sell.SetActive(true);
            buy.SetActive(false);
            sellOpen = true;
        }
    }

    public void SwitchBuy(){
        if(sellOpen){
            sell.SetActive(false);
            buy.SetActive(true);
            sellOpen = false;
        }
    }
}
