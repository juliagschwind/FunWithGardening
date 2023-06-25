using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestButton : MonoBehaviour
{
    public GameObject planted;

    public void harvest(){
        if(planted.TryGetComponent<Planted>(out Planted item)){
            item.Harvest();
        }
    }    
}
