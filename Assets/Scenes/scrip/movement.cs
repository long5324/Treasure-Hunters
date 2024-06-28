using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public float max_speed_move;
    public float min_speed_move;
    float curren_speed;
    public float baseJumpForce = 5f;
    bool is_set_over_speed = false;
    public int x { set; get; }
    public int y { set; get; }
    public bool is_jump { set; get; }
    public bool can_movement { set; get; }
    public Rigidbody2D rig { set; get; }
    public groudcheck check_ground;
    public bool delay_move { get; set; }

    void Start()
    {
        can_movement = true;
        rig = GetComponent<Rigidbody2D>();   
        delay_move = false;
    }
    // Update is called once per frame
    void Update()
    {
      if (delay_move) return;
      x= (int)Input.GetAxisRaw("Horizontal");
      y= (int)Input.GetAxisRaw("Vertical");
        bool ac = Input.GetKey(KeyCode.LeftShift);
        if(can_movement)
            if (!is_set_over_speed)
        if (!ac)
        {       
               
                curren_speed = min_speed_move;
        }
        else
        {
            curren_speed= max_speed_move;
        }
        rig.velocity = new Vector2(x * curren_speed, rig.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (check_ground.is_ground)
            {
                rig.velocity = new Vector2(rig.velocity.x, baseJumpForce);
            }
        }
    }
    public void set_over_speed(float speed)
    {
        curren_speed = speed;
        is_set_over_speed = true;
    }
    public void setnonespeed()
    {
        curren_speed = 0;
        is_set_over_speed = true;
    }
    public void end_over_speed()
    {
        is_set_over_speed = false;
    }
    private void FixedUpdate()
    {
        if (x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        is_jump = check_ground.is_ground;
    }
}
