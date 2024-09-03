using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public UIControl UIControl;
    public GameObject OBJEffectMovement;
    Animator AMTEffectMovement;
    public float max_speed_move;
    public float min_speed_move;
    float curren_speed;
    public float baseJumpForce = 5f;
    bool is_set_over_speed = false;
    public int x { set; get; }
    public bool is_jump { set; get; }
    public bool can_movement { set; get; }
    public Rigidbody2D rig { set; get; }
    public groudcheck check_ground;
    public bool delay_move { get; set; }
    Coroutine CoroutineSpeedUp;
    bool CheckFall=false;
    bool IsSpeedUp = false;
    bool IsSpeedDown = false;
    float BasicSpeedMin = 0;
    float BasicSpeedMax = 0;
    float BasicFoceJump = 0;
    void Start()
    {
        if(OBJEffectMovement != null) 
        AMTEffectMovement = OBJEffectMovement.GetComponent<Animator>();
        can_movement = true;
        rig = GetComponent<Rigidbody2D>();   
        delay_move = false;
        BasicFoceJump = baseJumpForce;
        BasicSpeedMax = max_speed_move;
        BasicSpeedMin = min_speed_move;
    }
    // Update is called once per frame
    void Update()
    {
        if (!can_movement) return;
            if (delay_move) return;
      x= (int)Input.GetAxisRaw("Horizontal");
        if(x!=0 && is_jump)
        {
            OBJEffectMovement.transform.position = gameObject.transform.position+new Vector3(-0.3f,0,0)*transform.localScale.x;
            OBJEffectMovement.transform.localScale = transform.localScale;
            
        }
        
    
      bool ac = Input.GetKey(KeyCode.LeftShift);
       
            if (!is_set_over_speed)
        if (!ac)
        {       
               
                curren_speed = min_speed_move;
                if (AMTEffectMovement != null)
                {
                    AMTEffectMovement.SetBool("Run", false);
                }
            }
        else
        {
            curren_speed= max_speed_move;
                if (AMTEffectMovement != null)
                {
                    AMTEffectMovement.SetBool("Run", true);
                }
            }
        rig.velocity = new Vector2(x * curren_speed, rig.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (is_jump)
            {
                if(OBJEffectMovement != null)
                OBJEffectMovement.transform.position = gameObject.transform.position;
                if (AMTEffectMovement != null)
                {
                    AMTEffectMovement.SetTrigger("Jump");
                }
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
       if(!is_jump)
        {
            CheckFall = true;
        }
        if (CheckFall && is_jump)
        {
            if (OBJEffectMovement != null)
                OBJEffectMovement.transform.position = gameObject.transform.position;
            if (AMTEffectMovement != null)
            {
                AMTEffectMovement.SetTrigger("Fall");
            }
            CheckFall = false;
        }
    }
    public void SpeedUp(float TimeSpeedUp)
    {
        if(IsSpeedUp )
        {
            StopCoroutine(CoroutineSpeedUp);
            CoroutineSpeedUp = StartCoroutine(WaitSpeedUp(TimeSpeedUp));
            UIControl.UseUiPooling("SpeedUp", TimeSpeedUp);
            return;
        }
        IsSpeedUp = true;
       
        max_speed_move = max_speed_move * 1.5f;
        min_speed_move = min_speed_move * 1.5f;
        CoroutineSpeedUp = StartCoroutine(WaitSpeedUp(TimeSpeedUp));
        UIControl.UseUiPooling("SpeedUp", TimeSpeedUp);
    }
    public void SpeedDown(float TimeSpeedDown)
    {
        if (IsSpeedDown)
        {
            StopCoroutine(CoroutineSpeedUp);
            CoroutineSpeedUp = StartCoroutine(WaitSpeedUp(TimeSpeedDown));
            UIControl.UseUiPooling("SpeedDown", TimeSpeedDown);
            return;
        }
        IsSpeedUp = true;
       
        max_speed_move = max_speed_move * 0.75f;
        min_speed_move = min_speed_move * 0.75f;
        baseJumpForce = baseJumpForce * 0.8f;
        CoroutineSpeedUp = StartCoroutine(WaitSpeedUp(TimeSpeedDown));
        UIControl.UseUiPooling("SpeedDown", TimeSpeedDown);
    }
    IEnumerator WaitSpeedUp(float time)
    {
        // Đợi 4 giây
        yield return new WaitForSeconds(time);

        IsSpeedUp = false;
        max_speed_move = BasicSpeedMax;
        min_speed_move = BasicSpeedMin;
        baseJumpForce = BasicFoceJump;
    }
}
