using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{

    public Transform position_shot;
    public float speed_attack;
    public float time_delay_attack_hit;
    public Transform paren;
     float dir_attack;
    public bool delay_attack { get; private set; } = false;
    Animator anim;
    Coroutine coroutine;
    private void Awake()
    {      
       
        dir_attack = -paren.transform.localScale.x;
        anim = GetComponent<Animator>(); 
    }
    private void OnEnable()
    {
        delay_attack = false;
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
    public void shot(){
        GameObject Amo = PoolingControler.Instance.SpawnAmo("Amo" + gameObject.tag);
        if (Amo == null)
        {
            return;
        }
        Amo.transform.position = position_shot.position;
        Amo.GetComponent<amo>().On(dir_attack);

    }
    public void DisRender()
    {
        SpriteRenderer rd = gameObject.GetComponent<SpriteRenderer>();
        if(rd != null )
        {
            rd.enabled = false;
        }
    }
}
