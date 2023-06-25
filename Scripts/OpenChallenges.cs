using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChallenges : MonoBehaviour
{
    public GameObject window;
    public static bool open = false;

    public void openProfile(){
        if(!open){
            window.SetActive(true);
            open = true;
        }else{
            window.SetActive(false);
            open = false;
        }
    }
}
