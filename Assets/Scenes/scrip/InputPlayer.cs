using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public groudcheck GroundCheck;
    public bool IsMove { get; private set; }
    public bool RightFace { get; private set; }
    public bool IsJump { get; private set; } = false;
    public bool IsRightMouse { get; private set; }
    public bool IsLeftMouse { get; private set; }
    private void Update()
    {
       int x = (int)Input.GetAxisRaw("Horizontal");
       int y = (int)Input.GetAxisRaw("Vertical");
        if(y > 0)
        {
            IsJump = true;
        }
        else { IsJump = false; }   
       if(x != 0) {
            IsMove = true;
            if(x < 0)
            {
                RightFace = false;
            }
            if(x > 0)
            {
                RightFace = true ;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            IsRightMouse = true;
        }
        else
        {
            IsRightMouse= false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            IsLeftMouse = true;
        }
        else
        {
            IsLeftMouse = false;
        }
    }
}
