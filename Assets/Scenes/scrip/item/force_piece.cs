using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_piece : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rig;
    public Vector2 force;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        if(rig != null )
        {
            rig.velocity = force;
        }
    }
}
