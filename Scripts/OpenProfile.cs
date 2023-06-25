using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenProfile : MonoBehaviour
{
    public GameObject profile;
    public GameObject lockedWater;
    public GameObject plant;
    public Planted planted;

    void Awake(){
        this.planted = plant.GetComponent<Planted>();
    }

    public void openProfile(){
        planted.initializeTraits();
        profile.SetActive(true);
    }

    public void closeProfile(){
        profile.SetActive(false);        
    }
}
