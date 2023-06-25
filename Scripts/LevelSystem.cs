using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int index;
    public int level;
    public static LevelSystem levelSystem;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI challenge1;
    public TextMeshProUGUI challenge2;
    public TextMeshProUGUI challenge3;

    public GameObject toggle1;
    public GameObject toggle2;
    public GameObject toggle3;

    public static List<Challenge> list1;
    public static List<Challenge> list2;
    public static List<Challenge> list3;
    
    private int goal1;
    private int progress1;
    private string target1;

    private int goal2;
    private int progress2;
    private string target2;

    private int goal3;
    private int progress3;

    private bool reached1;
    private bool reached2;
    private bool reached3;

    public Image unlocked1;
    public Image unlocked2;
    public TextMeshProUGUI unlocked1Name;
    public TextMeshProUGUI unlocked2Name;
    public TextMeshProUGUI title;
    public GameObject levelUpScreen;
    public static Store store;
    
    void Awake()
    {
        list1 = new List<Challenge>();
        list2 = new List<Challenge>();
        list3 = new List<Challenge>();

        store = gameObject.GetComponent<Store>();

        levelSystem = this;
        index = 0;
        level = 0;
        list1.Add(new Challenge("Tomato", 2)); 
        list1.Add(new Challenge("Pumpkin", 3));
        list1.Add(new Challenge("Strawberry", 5));

        list2.Add(new Challenge("Radish", 5));
        list2.Add(new Challenge("Spinach", 6));
        list2.Add(new Challenge("Carrot", 7));

        list3.Add(new Challenge("", 30));
        list3.Add(new Challenge("", 100));
        list3.Add(new Challenge("", 300));

    }

    public void levelup(){
        if(level>=3){
            Debug.Log("Highest Possible Level Reached");
            return;
        }
        goal1 = list1[index].count;
        target1 = list1[index].plant;
        progress1 = 0;
        goal2 = list2[index].count;
        target2 = list2[index].plant;
        progress2 = 0;
        goal3 = list3[index].count;
        progress3 = Coins.amount;

        challenge1.text = "Harvest the " + target1 + " " + goal1 + " times (" + progress1 + "/" + goal1 + ")";
        challenge2.text = "Harvest " + target2 + " with a Happiness Score over " + goal2 + " ("+ progress2 + "/" + goal2 + ")";
        challenge3.text = "Reach " + goal3 + " Coins (" + progress3 + "/" + goal3 + ")";
        index++;
        levelText.text = index.ToString();
        level++;

        reached1 = false;
        reached2 = false;
        reached3 = false;

        toggle1.GetComponent<Toggle>().isOn = false;
        toggle2.GetComponent<Toggle>().isOn = false;
        toggle3.GetComponent<Toggle>().isOn = false;
        Update3(Coins.amount);
        showLevelUp();
    }

    public void showLevelUp(){
        levelUpScreen.SetActive(true);
        int count = 1;
        title.text = "LEVEL " + this.level;
        Dictionary<string, Item> items = ItemStorage.getItems();
        foreach(KeyValuePair<string, Item> entry in items){
            Item current = entry.Value;
            if(current.level == this.level){
                if(count == 1){
                    count++;
                    unlocked1.sprite = current.stage3;
                    unlocked1Name.text = current.species;
                    if(0 == string.Compare(entry.Key, "Tomato")){
                        unlocked1.rectTransform.sizeDelta = new Vector2(190f, 100f);
                        unlocked1.transform.Translate(0.0f, 35f, 0.0f);
                    }
                }else{
                    unlocked2.sprite = current.stage3;
                    unlocked2Name.text = current.species;
                }
            }
        }
        
    }


    public void Update1(int amount, string species){
        if(string.Compare(species,target1) == 0 && !reached1){
            progress1 = amount;
            challenge1.text = "Harvest the " + target1 + " " + goal1 + " times (" + progress1 + "/" + goal1 + ")";
            if(progress1>=goal1){
                reached1 = true;
                toggle1.GetComponent<Toggle>().isOn = true;
                CheckLevelUp();
            }
        }
    }

    public void Update2(int amount, string species){
        if(string.Compare(species,target2) == 0 && !reached2){
            progress2 = amount;
            challenge2.text = "Harvest " + target2 + " with a Happiness Score over " + goal2 + " ("+ progress2 + "/" + goal2 + ")";
            if(progress2>=goal2){
                reached2 = true;
                toggle2.GetComponent<Toggle>().isOn = true;
                CheckLevelUp();
            }
        }
    }
    
    public void Update3(int amount){
        if(true){
            progress3 = amount;
            challenge3.text = "Reach " + goal3 + " Coins (" + progress3 + "/" + goal3 + ")";
            if(progress3>=goal3){
                reached3 = true;
                toggle3.GetComponent<Toggle>().isOn = true;
                CheckLevelUp();
            }else{
                toggle3.GetComponent<Toggle>().isOn = false;
            }
        }
    }

    void CheckLevelUp(){
        if(reached1 && reached2 && reached3){
            levelup();
        }
    }
}

public class Challenge{
    public string plant;
    public int count;

    public Challenge(string plant, int count){
        this.plant = plant;
        this.count = count;
    }
}
