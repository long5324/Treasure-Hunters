using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorItem : MonoBehaviour
{
     Animation IdelAnimation;
    private void Awake()
    {
        IdelAnimation = GetComponent<Animation>();
        IdelAnimation.Play("Idel");
    }
}
