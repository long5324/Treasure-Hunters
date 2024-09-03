using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groudcheck : MonoBehaviour
{
    public LayerMask layer_check;
    public bool is_ground { get; private set; } =false;
    private void OnTriggerStay2D(Collider2D other)
    {
      
        if (layer_check == (layer_check | (1 << other.gameObject.layer)))
        {
            is_ground = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (layer_check == (layer_check | (1 << other.gameObject.layer)))
        {
            is_ground = false;
        }
    }
}
