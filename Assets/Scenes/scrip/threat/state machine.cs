using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class statemachine : MonoBehaviour
{
     GameObject tagert;
    public Animator effect_skill;
    public LayerMask layerMaskplayer;
    public LayerMask layer_map;
    public float time_idel;
    public float defaul_speed;
    public float max_speed;
    [Space(10)]
    [Header("set up vison")]
    public bool draw_vision = false;
    public Vector2 size_vision;
    public Vector2 dir_vision;
    [Space(10)]
    [Header("set up attack")]
    public bool draw_attack_vison=false;
    public Vector2 size_attack_vision;
    public Vector2 dir_attack_vision;
    [Space(10)]
    [Header("attack")]
    public bool draw_fl_attack = false;
    public Vector2 size_fl_attack;
    public Vector2 dir_fl_attack;
    public bool now_attack { get; set; }
    Rigidbody2D rig;
    Animator animator;
    public bool on_attack { get; set; }
    public bool delay { set; get; }
    [System.Serializable]
    public struct vision_check_player
    {
        public string name;
        public bool draw;
        public Vector2 size;
        public Vector2 dir;
    }
    public enum state
    {
        takeaway,
        idel,
        run,
        attack,
        hit,
        death,
    }
    state state_threat = new state();

    private void Awake()
    {
        state_threat = state.takeaway;
        tagert = GameObject.Find("Player");
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public List<vision_check_player> ground_checkboxes;
    private void OnDrawGizmos()
    {
        foreach (var box in ground_checkboxes)
        {
            if (box.draw)
            {
                Gizmos.color = UnityEngine.Color.blue;
                Gizmos.DrawWireCube(new Vector2(transform.position.x + box.dir.x * transform.localScale.x, transform.position.y + box.dir.y), box.size);
            }
        }
        if (draw_vision)
        {
            Gizmos.color = UnityEngine.Color.blue;
            Gizmos.DrawWireCube(new Vector2(transform.position.x + dir_vision.x * transform.localScale.x, transform.position.y + dir_vision.y), size_vision);
        }
        if (draw_attack_vison)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(new Vector2(transform.position.x + dir_attack_vision.x * transform.localScale.x, transform.position.y + dir_attack_vision.y), size_attack_vision);
        }
        if (draw_fl_attack)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(new Vector2(transform.position.x + dir_fl_attack.x * transform.localScale.x, transform.position.y + dir_fl_attack.y), size_fl_attack); ;
        }
    }


    // Start is called before the first frame update
    void take_a_way()
    {
        if (rig != null)
        {
            rig.velocity = new Vector2(defaul_speed * transform.localScale.x, rig.velocity.y);
        }
    }
    public void trans_state(state state)
    {
        state_threat = state;
    }
    public void trans_state_attack()
    {
        state_threat = state.run;
    }
    public void trans_state_idel()
    {
        state_threat = state.idel;
        on_attack = false;
        rig.velocity = new Vector2(0, rig.velocity.y);
        animator.SetBool("attack_end", false);
        dir_attack = 0;
        now_attack = false;
    }
    public bool check_ground()
    {
        foreach (var box in ground_checkboxes)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * box.dir.x, box.dir.y) + (Vector2)gameObject.transform.position, box.size, 0f, layer_map);
            if (box.name == "check_horizontal")
            {
                if (hits.Length > 0)
                {

                    return true;
                }
            }
            if (box.name == "check_vertical")
            {
                if (hits.Length == 0)
                {
                    return true;
                }
            }

        }
        return false;
    }
    int dir_attack = 0;
    // Update is called once per frame
    private void LateUpdate()
    {
        Debug.Log(state_threat);
        if (delay)
        {
            return;
        }
        if (state_threat == state.takeaway)
        {
            if (vision_check()) {
                state_threat = state.run;
            }
            if (animator != null)
            {
                animator.SetBool("run", true);
            }
            rig.velocity = new Vector2(-transform.localScale.x * defaul_speed, rig.velocity.y);
            if (check_ground())
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
                state_threat = state.idel;
                animator.SetTrigger("idel");
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

        }
        else if(state_threat == state.idel){
            animator.SetBool("run", false);
            if (vision_check())
            {
                state_threat = state.run;
            }

        }
        else if (state_threat == state.run)
        {
            animator.SetBool("run", true);
            int dir = tagert.transform.position.x>transform.position.x?  1 :-1;
            transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            rig.velocity= new Vector2(max_speed*dir,rig.velocity.y);
            if (check_attack())
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
                state_threat = state.attack;
            }
            if (!vision_check_attack())
            {
              
                state_threat = state.takeaway;
            }
        }
        else if (state_threat == state.attack)
        {
            animator.SetBool("run", false);
            if (!check_attack() && !now_attack )
            {
                state_threat = state.run;
            }
            if (!now_attack&& check_attack())
            {
                now_attack = true;
                animator.SetTrigger("attack");
            }
            if (on_attack)
            {
                
                if (dir_attack == 0)
                {
                    dir_attack = tagert.transform.position.x > transform.position.x ? 1 : -1;
                }
                transform.localScale = new Vector2(dir_attack, transform.localScale.y);
                rig.velocity = new Vector2(max_speed * 2 * dir_attack, rig.velocity.y);
                if (check_ground())
                {
                    on_attack = false;
                    rig.velocity = new Vector2(0, rig.velocity.y);
                    animator.SetBool("attack_end", false);
                    dir_attack = 0;
                    now_attack = false;
                    state_threat = state.idel;
                }
            }

        }
       
    }

    public void Start_attack_start_pink()
    {
        on_attack = true;
        animator.SetBool("attack_end",true);    
    }
    bool vision_check()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * dir_vision.x, dir_vision.y) + (Vector2)gameObject.transform.position, size_vision, 0f, layerMaskplayer);
        if (hits.Length > 0)
        {
            return true;
        }
        return false;
    }
      bool vision_check_attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * dir_attack_vision.x, dir_attack_vision.y) + (Vector2)gameObject.transform.position, size_attack_vision, 0f, layerMaskplayer);
        if (hits.Length > 0)
        {
            return true;
        }
        return false;
    }
    bool check_attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * dir_fl_attack.x, dir_fl_attack.y) + (Vector2)gameObject.transform.position, size_fl_attack, 0f, layerMaskplayer);
        if (hits.Length > 0)
        {
            return true;
        }
        return false;
    }
    public void SetAttack(bool i)
    {
        now_attack = i;
    }
    public void EndAttack()
    {
        now_attack = false;
    }
    public void OnEffectSkill()
    {
        effect_skill.SetTrigger("attack");
    }
  
}
