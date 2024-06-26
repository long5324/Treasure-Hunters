using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iten_spaw : MonoBehaviour
{

    public GameObject Game_spawn;
    public void spawn()
    {
        Instantiate(Game_spawn);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
