using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PoolingControler : MonoBehaviour
{
    public static PoolingControler Instance;
    [SerializeField] public GameObject ObjectBooling;
    [System.Serializable]
    public class InfoBooling{
        public string ParentObject;
        public GameObject Amo;
        public int CountInitAmo;
    }
    
    // Start is called before the first frame update
    [SerializeField] List<InfoBooling> IFBooling;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Onsetup()
    {
        // Lưu danh sách các đối tượng con vào danh sách tạm thời
        var children = new List<GameObject>();
        foreach (Transform child in ObjectBooling.transform)
        {
            children.Add(child.gameObject);
        }

        // Xóa tất cả các đối tượng con
        foreach (GameObject child in children)
        {
            DestroyImmediate(child);
        }
        foreach (var item in IFBooling)
        {
            CountPrefabsInScene(item);
        }
       
       
    }
    void initAmo(InfoBooling tg)
    {
        for (int i = 0; i < tg.CountInitAmo; i++)
        {
            GameObject newObject = Instantiate(tg.Amo, ObjectBooling.transform.position, Quaternion.identity);
            newObject.transform.SetParent(ObjectBooling.transform);
            newObject.SetActive(false);
        }
    }
    void CountPrefabsInScene(InfoBooling tg)
    {
       
        if (tg.ParentObject == "")
        {
            initAmo(tg);
            return;
        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tg.ParentObject);
       
        // Kiểm tra từng đối tượng để xem có phải được tạo từ prefab hay không
        foreach (GameObject obj in objectsWithTag)
        {
           
            initAmo(tg);
            
        }
       
    }
    public GameObject SpawnAmo(string Tag)
    {
        foreach(Transform i in ObjectBooling.transform)
        {
            if (i.gameObject.CompareTag(Tag)&&!i.gameObject.activeSelf)
            {
                i.gameObject.SetActive(true);
                return i.gameObject;
            }
        }
        return null;
    }
}
