using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class heath : MonoBehaviour
{
    public float max_hp;
    public float curren_hp;
    public UnityEvent event_dame;
    public UnityEvent event_death;
    public float anti_nock;
    public float time_wait_attack;
    public bool can_dame { get; set; }
    public void dame_attack(float dame)
    {
        
        if (curren_hp == 0)
            return;
        if (curren_hp > dame)
        {
            curren_hp = curren_hp - dame;
            can_dame = false;
            StartCoroutine(wait_dame(time_wait_attack));
        }
        else if (curren_hp <= dame && curren_hp > 0)
        {
            curren_hp = 0;
            event_death.Invoke();
            return;
        }
        event_dame.Invoke();

    }
    public void destroy_object()
    {
        StartCoroutine(destroy_wait(5f));
    }
   public IEnumerator destroy_wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    public IEnumerator wait_dame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        can_dame = true;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        curren_hp = max_hp;
        can_dame = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
