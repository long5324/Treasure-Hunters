using System.Collections;
using System.Collections.Generic;
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
    public void dame_attack(float dame)
    {
        if (curren_hp == 0)
            return;
        if(curren_hp > dame)
        curren_hp = curren_hp-dame;
        else if(curren_hp<=dame && curren_hp>0){
             curren_hp=0;
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
    // Start is called before the first frame update
    private void Awake()
    {
        curren_hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
