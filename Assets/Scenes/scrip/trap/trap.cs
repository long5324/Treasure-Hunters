using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public Animator attack_effect;
    public float speed_attack;
    public float time_delay_attack_hit;
    
    public bool delay_attack { get; private set; } = false;
    Animator anim;
    Coroutine coroutine;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void LateUpdate()
    {

        if (!delay_attack)
        {
            delay_attack = true;
            if(anim != null)
            {
               anim.SetTrigger("attack");
                coroutine= StartCoroutine(Delay(speed_attack));
            }
        }
    }
    private IEnumerator Delay( float delay)
    {
        yield return new WaitForSeconds(delay);
        delay_attack = false ;
    }
    public void delay_attack_hit()
    {
        StopCoroutine(coroutine);
        delay_attack = true;
        coroutine = StartCoroutine(Delay(time_delay_attack_hit));
    } 
    public void on_effect_attack() {
        attack_effect.SetTrigger("attack");
    }

}
