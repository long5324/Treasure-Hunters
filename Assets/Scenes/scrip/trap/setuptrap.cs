using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setuptrap : MonoBehaviour
{
   public GameObject TommeBotoon;
   public GameObject TommeMid;
   public GameObject TommeTop;
   [Range(1, 3)]
   public int Height=1;
    private void Awake()
    {

        if (Height == 1)
        {
            
            TommeTop.transform.position = TommeBotoon.transform.position;
            TommeBotoon.SetActive(false);
            TommeMid.SetActive(false);  
        }
        if (Height == 2)
        {
            TommeMid.transform.position = TommeBotoon.transform.position;

            TommeBotoon.SetActive(false);
           
        }
    }
}
