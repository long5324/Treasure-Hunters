using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animator_skill : MonoBehaviour
{
    Animator amsk;
   
    void Start()
    {
        amsk = GetComponent<Animator>();    
    }
    public void endattack()
    {
        amsk.SetInteger("attack", 0);
        
    }
    // Update is called once per frame

}
