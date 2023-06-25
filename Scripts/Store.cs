using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{   
    public bool open = false;
    public GameObject shop;
    
    public void OpenStore(){
        shop.SetActive(true);
        open = true;
        StoreView.StartView();
        StoreView.StartSellView();
    }

    public void OnClick(){
        if(open){
            CloseStore();
        }else{
            OpenStore();
        }
    }

    public void CloseStore(){
        shop.SetActive(false);
        open = false;
        StoreView.CloseView();
        StoreView.CloseSellView();
    }
}
