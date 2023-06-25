using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButton : MonoBehaviour
{
    public GameObject WaterController;

    public void Water(){
        WaterController.GetComponent<Watering>().Water();
    }
}
