using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax_bg : MonoBehaviour
{
    [System.Serializable]
    public class BackGround {
        public GameObject Bk;
        public Material mr_bk;
        public float distance;
        [Range(0f, 1f)] public float speed=0.2f;

    }
    private void Awake()
    {
        foreach (BackGround backGround in backGroundList)
        {
            backGround.mr_bk = backGround.Bk.GetComponent<Renderer>().material;

        }
    }
    public List<BackGround> backGroundList;
    private void Update()
    {
        foreach(BackGround backGround in backGroundList)
        {
            backGround.distance += Time.deltaTime * backGround.speed;
            if (backGround.mr_bk != null)
            {
                backGround.mr_bk.SetTextureOffset("_MainTex", Vector2.right * backGround.distance);
                Debug.Log(1);
            }

        }
    }
}
