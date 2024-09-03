using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class paralax_bg : MonoBehaviour
{
    public Transform Camera;
    Vector2 LastPositionCamera;

    [System.Serializable]
    public class ObParalax {
        public GameObject ObjectBackGround;
        public float Speed;
        public Vector2 ParaTrans;
        public float TextureSizex { get; set; }
        public bool OnReBack = true;
    }
    [System.Serializable]
    public class RandomSpawn
    {
        public GameObject ObjectBackGround;
        public Vector2 PostionRandom;
        public float Size;
        public bool ondraw = false;
    }
    public List<RandomSpawn> RandomSpawnList;
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        foreach (var spawn in RandomSpawnList)
        {
            if (!spawn.ondraw)
                continue;
            Gizmos.DrawLine(new Vector3(spawn.PostionRandom.x, spawn.PostionRandom.y), new Vector3(spawn.PostionRandom.x, spawn.PostionRandom.y + spawn.Size));
        }
    }
    public List<ObParalax> obParalaxList;
    private void Awake()
    {
        LastPositionCamera = Camera.position;
        foreach (var obParalax in obParalaxList)
        {
            Sprite tg = obParalax.ObjectBackGround.GetComponent<SpriteRenderer>().sprite;
            obParalax.TextureSizex = tg.texture.width / tg.pixelsPerUnit;
        }
    }
    private void Update()
    {
        Vector2 deltapositionCamera = (Vector2)Camera.position - LastPositionCamera;
        foreach (ObParalax go in obParalaxList) {
            go.ObjectBackGround.transform.position += new Vector3(deltapositionCamera.x * go.ParaTrans.x, deltapositionCamera.y * go.ParaTrans.y);
            go.ObjectBackGround.transform.position += new Vector3(-Time.deltaTime * go.Speed, 0, 0);
            if (!go.OnReBack)
                continue;
            if (math.abs(Camera.position.x - go.ObjectBackGround.transform.position.x) >= go.TextureSizex)
            {
                float offset = (Camera.position.x - go.ObjectBackGround.transform.position.x) % go.TextureSizex;
                go.ObjectBackGround.transform.position = new Vector2(offset + Camera.position.x, go.ObjectBackGround.transform.position.y);
            }
        }

        LastPositionCamera = Camera.position;
    }
    void random()
    {

    }
}
