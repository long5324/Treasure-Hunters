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
    private void OnValidate()
    {
       
        if (TommeTop == null || TommeBotoon == null || TommeMid == null)
        {
            Debug.LogWarning("Một hoặc nhiều GameObject chưa được gán.");
            return;
        }
        // Thực hiện các thay đổi trạng thái và vị trí an toàn
        switch (Height)
        {
            case 1:
                TommeBotoon.SetActive(false);
                TommeMid.SetActive(false);
                break;
            case 2:
                TommeBotoon.SetActive(false);
                TommeMid.SetActive(true);
                break;

            default:
                TommeBotoon.SetActive(true);
                TommeMid.SetActive(true);
                break;
        }
    }

}
