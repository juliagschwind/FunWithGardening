using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event {}

public class newBalance : Event{
    public int coins;
    public newBalance(int coins){
        this.coins = coins;
    }
}

public class newItem : Event{
    public int amount;
    public string name;
    public newItem(int amount, string name){
        
        this.amount = amount;
        this.name = name;
    }
}
