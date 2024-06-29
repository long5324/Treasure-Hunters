using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groudcheck : MonoBehaviour
{
    public bool is_ground = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng có layer là "map"
        if (other.gameObject.layer == LayerMask.NameToLayer("map"))
        {
            is_ground = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("map"))
        {
            is_ground = false;
        }
    }
}
