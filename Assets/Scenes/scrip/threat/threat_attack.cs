using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class attack : MonoBehaviour
{
    public LayerMask layerMaskPlayer;
    [System.Serializable]
    public struct attack_if
    {
        [Header("info_skill")]
        public string name;
        public float dame;
        public Vector2 size;
        public Vector2 dir;
        public bool draw;
        [Space(10)]
        [Header("nockback_machine")]
        public Vector2 force_nock;
        public Vector2 force_death;
        public float time_cc;
    }
    public List<attack_if> threat_attack;

    public void OnDrawGizmos()
    {
        foreach (var item in threat_attack)
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
    private void LateUpdate()
    {
        foreach (var item in threat_attack)
        {
            if (item.name == "collision")
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * item.dir.x, item.dir.y) + (Vector2)gameObject.transform.position, item.size, 0f, layerMaskPlayer);
                if(hits.Length == 0) { return; }
                foreach (var other in hits)
                {
                    heath player_heath = other.GetComponent<heath>();
                    if (player_heath != null && player_heath.can_dame)
                    {
                       
                        movement player_move = other.GetComponent<movement>();
                        if (player_move != null)
                        {
                            player_move.delay_move = true;
                            player_move.rig.velocity = Vector2.zero;
                            float dir = transform.position.x > other.transform.position.x ? 1 : -1;
                            if (player_heath.curren_hp > item.dame)
                                player_move.rig.velocity = new Vector2((item.force_nock.x - player_heath.anti_nock) * dir, item.force_nock.y - player_heath.anti_nock);
                            else
                            {
                                player_move.rig.velocity = new Vector2((item.force_death.x - player_heath.anti_nock) * dir, item.force_death.y - player_heath.anti_nock);
                            }
                            StartCoroutine(ResetDelay(player_move, item.time_cc-player_heath.anti_nock));
                            player_heath.dame_attack(item.dame);
                        }
                    }
                }
            }
        }
        }
    void Attack_check()
    {
        foreach (var item in threat_attack)
        {
            if (item.name == "attack")
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.localScale.x * item.dir.x, item.dir.y) + (Vector2)gameObject.transform.position, item.size, 0f, layerMaskPlayer);
                if (hits.Length == 0) { return; }
                foreach (var other in hits)
                {
                    heath player_heath = other.GetComponent<heath>();
                    if (player_heath != null && player_heath.can_dame)
                    {

                        movement player_move = other.GetComponent<movement>();
                        if (player_move != null)
                        {
                            player_move.delay_move = true;
                            player_move.rig.velocity = Vector2.zero;
                            float dir = transform.position.x > other.transform.position.x ? 1 : -1;
                            if (player_heath.curren_hp > item.dame)
                                player_move.rig.velocity = new Vector2((item.force_nock.x - player_heath.anti_nock) * -dir, item.force_nock.y - player_heath.anti_nock);
                            else
                            {
                                player_move.rig.velocity = new Vector2((item.force_death.x - player_heath.anti_nock) * -dir, item.force_death.y - player_heath.anti_nock);
                            }
                            StartCoroutine(ResetDelay(player_move, item.time_cc - player_heath.anti_nock));
                            player_heath.dame_attack(item.dame);
                        }
                    }
                }
            }
        }
    }
    private IEnumerator ResetDelay(movement state, float delay)
    {
        yield return new WaitForSeconds(delay);
        state.delay_move = false;
    }
}
