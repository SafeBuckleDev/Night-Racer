using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonaplication : MonoBehaviour
{
    public string url;
   
   

   

    public void openURL()
    {

        Application.OpenURL(url);

    }

  
}
