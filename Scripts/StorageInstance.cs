using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class StorageInstance : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI count;
    public TextMeshProUGUI species;
    

    public void InitializeInstance(Sprite sprite, string name, int count){
        img.sprite = sprite; 
        this.count.text = count.ToString();
        this.species.text = name;
    }
}
