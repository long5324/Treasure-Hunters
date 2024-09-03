using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class animator : MonoBehaviour
{

    movement mov_player;
    Animator at_player;
    void Start()
    {
        mov_player = GetComponent<movement>();
        at_player = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        at_player.SetBool("Run", mov_player.x!=0);
        at_player.SetBool("jump", mov_player.is_jump);
        at_player.SetFloat("velocityy", mov_player.rig.velocity.y);
        at_player.SetFloat("SpeedMove", mov_player.rig.velocity.x/4);
    }
}
