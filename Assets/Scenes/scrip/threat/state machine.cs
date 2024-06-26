using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class statemachine : MonoBehaviour
{
    public LayerMask layer_map;
    public Vector2 size_way;
    public float time_idel;
    Vector2 defaul_position;
    public float defaul_speed;
    public float max_speed;
    Rigidbody2D rig;
    Animator animator;
    public bool delay { set; get; }
    [System.Serializable]
     public struct ground_check_box{
        public string name;
        public bool draw;
        public Vector2 size;
        public Vector2 dir;
    }
    public List<ground_check_box> ground_checkboxes;
    private void OnDrawGizmos()
    {
        foreach(var box in ground_checkboxes)
        {
            if (box.draw)
            {
                Gizmos.color = UnityEngine.Color.blue;
                Gizmos.DrawWireCube(new Vector2(transform.position.x + box.dir.x * transform.localScale.x, transform.position.y + box.dir.y), box.size);
            }
        }
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, size_way);
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
        defaul_position = transform.position;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void take_a_way()
    {
        if(rig != null)
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
        foreach( var box in ground_checkboxes)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * box.dir.x, box.dir.y) + (Vector2)gameObject.transform.position, box.size, 0f, layer_map);
            if (box.name == "check_horizontal")
            {
                if (hits.Length > 0)
                {
                    
                    return true;        
                }
            }
            if(box.name == "check_vertical")
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
        if (delay)
        {
            return;
        }
        if (state_threat == state.takeaway)
        {
            if(animator != null)
            {
                animator.SetBool("run", true);
            }
            rig.velocity = new Vector2(-transform.localScale.x*defaul_speed, rig.velocity.y);
            if (check_ground())
            {
                Debug.Log("ok");
                rig.velocity = new Vector2(0, rig.velocity.y);
                animator.SetBool("run", false);
                state_threat = state.idel;
                animator.SetTrigger("idel");
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            
        }
    }
}
