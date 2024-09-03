using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class CullingControler : MonoBehaviour

{

    [SerializeField] Transform PositionPlayer;
    [SerializeField] LayerMask LayerObjectCulling;
    [SerializeField] bool OnDraw = false;
    [SerializeField] Vector2 SizeCheckCulling;
    List<GameObject> ObjectCulling = new List<GameObject>();
    private void Awake()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if ((LayerObjectCulling.value & (1 << obj.layer)) != 0)
            {
                ObjectCulling.Add(obj);
                obj.SetActive(false);
            }
        }

    }
    private void OnDrawGizmos()
    {
        if(!OnDraw) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SizeCheckCulling);
    }
    private void OnValidate()
    {
        transform.position = PositionPlayer.position;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i< ObjectCulling.Count;i++)
        {
            if (ObjectCulling[i] == null)
            {
                ObjectCulling.Remove(ObjectCulling[i]);
                continue;
            }
            if (ObjectCulling[i].activeSelf)
            {
                if(ObjectCulling[i].transform.position.x > gameObject.transform.position.x + SizeCheckCulling.x/2 || ObjectCulling[i].transform.position.x < gameObject.transform.position.x - SizeCheckCulling.x / 2)
                {
                    ObjectCulling[i].SetActive(false);
                }
            }
            else
            {
                if (ObjectCulling[i].transform.position.x > gameObject.transform.position.x - SizeCheckCulling.x / 2 && ObjectCulling[i].transform.position.x < gameObject.transform.position.x + SizeCheckCulling.x / 2)
                {
                    heath Health = ObjectCulling[i].GetComponent<heath>();
                    if (Health != null && Health.Death)
                        return;
                    ObjectCulling[i].SetActive(true);
                }
            }
        }
        transform.position = PositionPlayer.position;
    }
}
