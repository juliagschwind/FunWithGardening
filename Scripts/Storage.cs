using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static bool open = false;
    public GameObject storage;
    public GameObject button;

    public void OpenStorage(){
        storage.SetActive(true);
        open = true;
        button.SetActive(false);
        StorageView.StartView();

    }

    public void OnClick(){
        if(open){
            CloseStorage();
        }else{
            OpenStorage();
        }
    }

    public void CloseStorage(){
        storage.SetActive(false);
        open = false;
        button.SetActive(true);
        StorageView.CloseView();

    }
}
