using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class statemachine : MonoBehaviour
{
    public LayerMask layerMaskplayer;
    public LayerMask layer_map;
    public float time_idel;
    public float defaul_speed;
    public float max_speed;
    [Space(10)]
    [Header("set up vison")]
    public bool draw_vision;
    public Vector2 size_vision;
    public Vector2 dir_vision;
    [Space(10)]
    [Header("set up attack")]
    GameObject tagert;
    public bool draw_attack_vison;
    public Vector2 size_attack_vision;
    public Vector2 dir_attack_vision;
    public bool draw_attack;
    public Vector2 size_attack;
    public Vector2 dir_attack;
    Rigidbody2D rig;
    Animator animator;
    public bool delay { set; get; }
    [System.Serializable]
    public struct vision_check_player
    {
        public string name;
        public bool draw;
        public Vector2 size;
        public Vector2 dir;
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
        if (draw_attack)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(new Vector2(transform.position.x + dir_attack.x * transform.localScale.x, transform.position.y + dir_attack.y), size_attack);
        }
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
    state state_threat;
    private void Awake()
    {
        state_threat = state.takeaway;

        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
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

                }
            }

        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (state_threat == state.takeaway)
        {
            vision_check();
        }
    }
    private void LateUpdate()
    {

        if (delay)
        {
            return;
        }
        if (state_threat == state.takeaway)
        {
            if (animator != null)
            {
                animator.SetBool("run", true);
            }
            rig.velocity = new Vector2(-transform.localScale.x * defaul_speed, rig.velocity.y);
            if (check_ground())
            {

                rig.velocity = new Vector2(0, rig.velocity.y);
                animator.SetBool("run", false);
                state_threat = state.idel;
                animator.SetTrigger("idel");
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

        }
        else if (state_threat == state.attack)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * dir_attack_vision.x, dir_attack_vision.y) + (Vector2)gameObject.transform.position, size_attack_vision, 0f, layerMaskplayer);
           if(h)
            if (tagert == null)
            {
                foreach (var other in hits)
                {
                    tagert = other.gameObject;
                }
            }
        }
       
    }
    void vision_check()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * dir_vision.x, dir_vision.y) + (Vector2)gameObject.transform.position, size_vision, 0f, layerMaskplayer);
        if (hits.Length > 0)
        {
            state_threat = state.attack;
        }
    }
}
