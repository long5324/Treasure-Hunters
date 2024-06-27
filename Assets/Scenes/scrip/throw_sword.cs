using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class throw_sword : MonoBehaviour
{
    public int dir { get; set; }
    public float dame;
    public Vector2 force_nock;
    public Vector2 force_nock_death;
    public LayerMask layerMaskThreat;
    public float speed;
    Rigidbody2D rb;
    Animator ant;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ant = GetComponent<Animator>();
    }
    public void start_throw()
    {
        transform.localScale = new Vector2(dir, 1);
        rb.velocity = new Vector2 (speed*dir, 0);
    }


            /*heath threat_heath = collision.gameObject.GetComponent<heath>();
            if (threat_heath != null)
            {
                if (threat_heath.curren_hp > 0)
                {

                    Rigidbody2D rig_threat = collision.gameObject.GetComponent<Rigidbody2D>();
                    statemachine state_threat = collision.GetComponent<statemachine>();
                    if (rig_threat != null)
                    {
                        if (state_threat != null)
                        {
                            state_threat.delay = true;
                            StartCoroutine(ResetDelay(state_threat, 1f));
                        }
                        rig_threat.velocity = new Vector2(0, rig_threat.velocity.y);
                        if (threat_heath.curren_hp <= dame)
                        {
                            rig_threat.velocity = new Vector2((force_nock.x - threat_heath.anti_nock) * dir, force_nock.y - threat_heath.anti_nock);
                        }
                        else
                            rig_threat.velocity = new Vector2((force_nock_death.x - threat_heath.anti_nock) * dir, force_nock_death.y - threat_heath.anti_nock);
                    }
                    threat_heath.dame_attack(dame);

                }
            }*/

        
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng có layer là "map"
        if (other.gameObject.layer == LayerMask.NameToLayer("map"))
        {
            
            ant.SetTrigger("end");
            rb.velocity = Vector2.zero;
        }
         if ((layerMaskThreat & (1 << other.gameObject.layer)) != 0)
        {
            heath threat_heath = other.gameObject.GetComponent<heath>();
            if (threat_heath != null && threat_heath.can_dame)
            {
                if (threat_heath.curren_hp > 0)
                {

                    Rigidbody2D rig_threat = other.gameObject.GetComponent<Rigidbody2D>();
                    statemachine state_threat = other.GetComponent<statemachine>();
                    if (rig_threat != null)
                    {
                        if (state_threat != null)
                        {
                            state_threat.delay = true;
                            StartCoroutine(ResetDelay(state_threat, 1f));
                        }
                        rig_threat.velocity = new Vector2(0, rig_threat.velocity.y);
                        if (threat_heath.curren_hp <= dame)
                        {
                            rig_threat.velocity = new Vector2((force_nock.x - threat_heath.anti_nock) * dir, force_nock.y - threat_heath.anti_nock);
                        }
                        else
                            rig_threat.velocity = new Vector2((force_nock_death.x - threat_heath.anti_nock) * dir, force_nock_death.y - threat_heath.anti_nock);
                    }
                    threat_heath.dame_attack(dame);

                }
            }
        }

    }
    private IEnumerator ResetDelay(statemachine state, float delay)
    {
        yield return new WaitForSeconds(delay);
        state.delay = false;
    }
}
