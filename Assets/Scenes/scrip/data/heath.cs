using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class heath : MonoBehaviour
{
    public UIControl HealBarControler;
    public GameObject UIHealBar;
    public float max_hp;
    public float curren_hp;
    public UnityEvent event_dame;
    public UnityEvent event_death;
    public float anti_nock;
    public float time_wait_attack;
    public bool Death { get; set; } = false; 
    public bool can_dame { get; set; }
    SpriteRenderer rend;
    bool OnPoisoned = false;
    float TimePoisoned;
    public void dame_attack(float dame)
    {
        
        if (curren_hp == 0)
            return;
        if (curren_hp > dame)
        {
            
            if(UIHealBar!=null && !UIHealBar.activeSelf)
            {
                UIHealBar.SetActive(true);
            }
            curren_hp = curren_hp - dame;
            if (HealBarControler != null)
            {
                HealBarControler.ChangeHealthFilled("Health", max_hp, curren_hp, false);
            }
            can_dame = false;
            if (rend != null)
            {   
                float endAlpha = 0.8f;  // Giá trị alpha mục tiêu là 50%
                Color originalColor = rend.material.color;
                Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
                rend.material.color = newColor;
                StartCoroutine(wait_dame(time_wait_attack, originalColor));
            }
        }
        else if (curren_hp <= dame && curren_hp > 0)
        {
            curren_hp = 0;
            if(HealBarControler!=null)
            HealBarControler.ChangeHealthFilled("Health",max_hp, 0,false);
            Death = true;
            event_death.Invoke();
            return;
        }
        event_dame.Invoke();
    }
    public void destroy_object(float time_wait_destroy)
    {
        StartCoroutine(destroy_wait(time_wait_destroy));
    }
   public IEnumerator destroy_wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    public IEnumerator wait_dame(float waitTime ,Color cl)
    {
        yield return new WaitForSeconds(waitTime);
        can_dame = true;
        rend.material.color = cl;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        curren_hp = max_hp;
        can_dame = true;
        rend = GetComponent<SpriteRenderer>();
    }
   public void PoisonedEffect(float TimeEffect)
    {
        OnPoisoned = true;
        TimePoisoned = TimeEffect;
        if (OnPoisoned)
        {
            StartCoroutine(WaitPoisoned(1));
            HealBarControler.UseUiPooling("Poisoned", TimeEffect);
        }
    }
    IEnumerator WaitPoisoned(float time)
    {
        while (TimePoisoned > 0)
        {
            // Đợi thời gian đã định
            yield return new WaitForSeconds(time);

            // Giảm thời gian bị nhiễm độc và máu hiện tại
            TimePoisoned -= time;
            curren_hp -= 1;

            // Cập nhật thanh máu
           
            HealBarControler.ChangeHealthFilled("Health", max_hp, curren_hp, false);
        }

        // Kết thúc nhiễm độc
        OnPoisoned = true;
    }

}
