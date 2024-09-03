using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControler : MonoBehaviour
{
    [SerializeField] LayerMask LaymaskCanHit;
    [SerializeField]bool Health = false;
    [SerializeField] float ValueHeal=0;
    [SerializeField] bool SpeedUp = false;
    [SerializeField] float TimeSpeedUp = 0;
    heath PlayerHealth;
    movement PlayerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Health) 
        if (LaymaskCanHit == (LaymaskCanHit | (1 << collision.gameObject.layer)))
        {
            HealthPointSetup(collision);
            GameObject EffectPoint = PoolingControler.Instance.SpawnAmo("EffectPoint");
            EffectPoint.transform.position = transform.position;
            Destroy(gameObject);
        }
        if (SpeedUp)
        {
            if (LaymaskCanHit == (LaymaskCanHit | (1 << collision.gameObject.layer)))
            {
                PlayerMovement = collision.GetComponent<movement>();

                if (PlayerMovement != null)
                    PlayerMovement.SpeedUp(TimeSpeedUp);
                GameObject EffectPoint = PoolingControler.Instance.SpawnAmo("EffectPoint");
                EffectPoint.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }
    void HealthPointSetup(Collider2D collision)
    {
        if (Health)
        {

            if (PlayerHealth == null)
            {
                PlayerHealth = collision.gameObject.GetComponent<heath>();

            }
            if (PlayerHealth != null)
            {
                if (PlayerHealth.curren_hp + ValueHeal <= PlayerHealth.max_hp)
                    PlayerHealth.curren_hp += ValueHeal;
                else
                {
                    PlayerHealth.curren_hp = PlayerHealth.max_hp;
                }
                PlayerHealth.HealBarControler.ChangeHealthFilled("",PlayerHealth.max_hp, PlayerHealth.curren_hp,false);
            }
        }
    }
}
