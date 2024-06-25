using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafolow : MonoBehaviour
{

    public GameObject Player;
    public Vector2 set_dire;
    public Vector2 size_free;
    public Vector2 dire_free;
    public bool set_fl=false;
    public bool draw_camera = false;
    // Start is called before the first frame update
    void Start()
    {
       

    }
    private void LateUpdate()
    {
       if(set_fl)
       transform.position = new Vector3(Player.transform.position.x +set_dire.x, Player.transform.position.y+set_dire.y, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        if (!draw_camera) return;
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 2;

        // Lấy chiều rộng của camera dựa trên tỷ lệ khung hình (aspect ratio)
        float  cameraWidth = cameraHeight * cam.aspect;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(Player.transform.position.x+set_dire.x,Player.transform.position.y+set_dire.y), new Vector2(cameraWidth,cameraHeight));
        Gizmos.DrawWireCube(Player.transform.position+ new Vector3(dire_free.x,dire_free.y,0), size_free);
    }
}
