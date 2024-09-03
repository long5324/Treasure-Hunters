using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [System.Serializable]
    public class UIInfo
    {
        [SerializeField] public string NameUi;
        [SerializeField] public GameObject ObjectBar;
        [SerializeField] public Bar IFHealthBar;
    }
    [System.Serializable]
    public class UIInfoPooling
    {
        [SerializeField]  public string NameUi;
        [SerializeField] public Bar IfBar;
        public bool IsCount { get; set; } = false;
        public GameObject ObjUiPooling { get; set; } 
        public float TimeCountDow { get; set; } = 0;
        public float StepTimeCountDow { get; set; } = 0;
    }
    [System.Serializable]
    public class ObjectPoolingUi
    {
        public GameObject ObjUiPooling;
        public UIInfoPooling Info { get;set; }
    }
    public List<UIInfoPooling> ListUIInfoLooping = new List<UIInfoPooling>();
    public List<ObjectPoolingUi> ListObjectPoolingUi = new List<ObjectPoolingUi>();
    public List<UIInfo> Info;
    public void UseUiPooling(string Name,float TimeCount)
    {
        foreach (var bar1 in ListUIInfoLooping)
        {
            if (bar1.NameUi == Name)
            {
                if (bar1.IsCount)
                {
                    bar1.TimeCountDow = TimeCount;
                    bar1.StepTimeCountDow = TimeCount;
                    return;
                }

            }
        }
        foreach (var pool in ListObjectPoolingUi)
        {
            if (!pool.ObjUiPooling.activeSelf)
            {
                
                pool.ObjUiPooling.SetActive(true);
               
                foreach (var bar in ListUIInfoLooping)
                {
                   
                    if (bar.NameUi == Name)
                    {
                       
                        bar.TimeCountDow = TimeCount;
                        bar.StepTimeCountDow = TimeCount;
                        SetImageUI(pool.ObjUiPooling, bar);
                        bar.IsCount = true;
                        bar.ObjUiPooling = pool.ObjUiPooling;
                        pool.Info = bar;
                        return;
                    }
                   
                   
                }
            }
        }
    }
    public void SetImageUI(GameObject Obj, UIInfoPooling Info)
    {
        Transform objectBarTransform = Obj.transform;
        if (objectBarTransform.childCount >= 4)
        {
            objectBarTransform.GetChild(0).GetComponent<Image>().sprite = Info.IfBar.BackGroundHealth[0];
            objectBarTransform.GetChild(1).GetComponent<Image>().sprite = Info.IfBar.BackGroundHealth[1];
            objectBarTransform.GetChild(2).GetComponent<Image>().sprite = Info.IfBar.BackGroundHealth[2];
            objectBarTransform.GetChild(3).GetComponent<Image>().sprite = Info.IfBar.HealthFilled;
        }
    }
    private void Update()
    {

        foreach(var bar in ListUIInfoLooping)
        {
            if(bar.IsCount) { 
               if(bar.StepTimeCountDow > 0)
                {
                    bar.StepTimeCountDow -= Time.deltaTime;
                    bar.ObjUiPooling.transform.GetChild(3).GetComponent<Image>().fillAmount = bar.StepTimeCountDow/bar.TimeCountDow;
                }
                else {
                    bar.IsCount = false;
                    bar.ObjUiPooling.SetActive(false);
                    UpdatePosionPoolingIu();
                }
            }
        }
    }
    void UpdatePosionPoolingIu()
    {
        
        for (int i = 0; i < ListObjectPoolingUi.Count-1; i++)
        {
            if (!ListObjectPoolingUi[i].ObjUiPooling.activeSelf)
            {
                
                for (int j = i; j < ListObjectPoolingUi.Count -1 ; j++)
                {
                    ObjectPoolingUi tg1 = ListObjectPoolingUi[j+1];
                    ListObjectPoolingUi[j + 1] = ListObjectPoolingUi[j];
                    ListObjectPoolingUi[j] = tg1;


                    Vector3 tgP = ListObjectPoolingUi[j + 1].ObjUiPooling.transform.position;
                    ListObjectPoolingUi[j + 1].ObjUiPooling.transform.position = ListObjectPoolingUi[j].ObjUiPooling.transform.position;
                    ListObjectPoolingUi[j].ObjUiPooling.transform.position = tgP;
                   
                  
                }
            }
        }
    }

    public void ChangeHealthFilled(string NameUI, float MaxHealth, float CurrentHealth,bool Pooling)
    {
        if(Pooling)
        {
            foreach (var info in ListUIInfoLooping)
            {

                if (info.NameUi == NameUI)
                {
                    
                    Image healthImage = info.ObjUiPooling.transform.GetChild(3).GetComponent<Image>();
                    healthImage.fillAmount = CurrentHealth / MaxHealth;
                    return;
                }
            }
        }
        foreach (UIInfo info in Info)
        {

            if ( info.NameUi == NameUI )
            {
                    Image healthImage = info.ObjectBar.transform.GetChild(3).GetComponent<Image>();

                        healthImage.fillAmount = CurrentHealth / MaxHealth;
                return;
            }
        }
    }


    public void SetActiveUi(String NameUI,bool IsActive)
    {
        foreach (UIInfo info in Info)
        {
        if(info.NameUi == NameUI )
            {
                info.ObjectBar.SetActive(IsActive);
            }
        }
     }
    public bool GetActiveUi(string NameUI)
    {
        foreach (UIInfo info in Info)
        {
            if (info.NameUi == NameUI)
            {
                return info.ObjectBar.activeSelf;
            }
        }
        return false;
    } 

    private void Awake()
    { 
        foreach (UIInfo info in Info)
        {
            if (info != null && info.ObjectBar != null && info.IFHealthBar != null)
            {
                Transform objectBarTransform = info.ObjectBar.transform;
                if (objectBarTransform.childCount >= 4)
                {
                    objectBarTransform.GetChild(0).GetComponent<Image>().sprite = info.IFHealthBar.BackGroundHealth[0];
                    objectBarTransform.GetChild(1).GetComponent<Image>().sprite = info.IFHealthBar.BackGroundHealth[1];
                    objectBarTransform.GetChild(2).GetComponent<Image>().sprite = info.IFHealthBar.BackGroundHealth[2];
                    objectBarTransform.GetChild(3).GetComponent<Image>().sprite = info.IFHealthBar.HealthFilled;
                }  
            }
        }

    }
}
