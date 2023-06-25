using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public Canvas canvas;

    private void Awake(){
        game = this;
        DragItem.canvas = canvas;
    }

    public static void UpdateCoins(int amount){
        newBalance newEvent = new newBalance(amount);
        (EventManager.s_Instance).QueueEvent(newEvent);
    }

    public static void UpdateItem(int amount, string name){
        newItem newEvent = new newItem(amount, name);
        (EventManager.s_Instance).QueueEvent(newEvent);
    }
}
