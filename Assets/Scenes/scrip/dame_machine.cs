using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Color;
using static UnityEngine.UI.Image;

public class dame_machine : MonoBehaviour
{
    public LayerMask layerMaskthreat;
   
    [System.Serializable]
   public struct if_skill {
        [Header("info_skill")]
        public string name_skill;
       public float dame ;
       public Vector2 size;
       public Vector2 dir;
       public bool draw;
        [Space(10)]
        [Header("nockback_machine")]
        public Vector2 foce_nock;
       public UnityEvent ev_after_nock;
       public UnityEvent ev_before_nock;
    }
    [System.Serializable]
    public struct if_nockback
    {
        
    }
    private void Awake()
    {
    }
    public List<if_skill> list_skill = new List<if_skill>();
    public void OnDrawGizmos()
    {
        foreach (var item in list_skill)
        {
            if (item.draw)
            {
                Gizmos.color = UnityEngine.Color.red;
                 Vector2 origin = new Vector2(transform.localScale.x * item.dir.x, item.dir.y) + (Vector2)gameObject.transform.position;
                // Vẽ BoxCast
                Gizmos.DrawWireCube(origin, item.size);
            }
        }
        
    }
    public void check(string name)
    {
        foreach (var item in list_skill)
        {
            if (item.name_skill == name)
            { 
                Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * item.dir.x, item.dir.y) + (Vector2)gameObject.transform.position,item.size,0f,layerMaskthreat);
                foreach(var hit in hits)
                {
                   heath threat_heath = hit.gameObject.GetComponent<heath>();
                    if(threat_heath != null)
                    {
                        if(threat_heath.curren_hp > 0)
                        {
                            threat_heath.dame_attack(item.dame);
                            Rigidbody2D rig_threat = hit.gameObject.GetComponent<Rigidbody2D>();
                            statemachine state_threat = hit.GetComponent<statemachine>();
                            if (rig_threat != null)
                            {
                                if(state_threat!= null)
                                {
                                    state_threat.delay = true;
                                    StartCoroutine(ResetDelay(state_threat, 2f));
                                }
                                rig_threat.velocity = new Vector2(0,rig_threat.velocity.y);
                                rig_threat.velocity = new Vector2((item.foce_nock.x - threat_heath.anti_nock) * transform.localScale.x, item.foce_nock.y-threat_heath.anti_nock);
                            }

                        }
                    }
                   
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
