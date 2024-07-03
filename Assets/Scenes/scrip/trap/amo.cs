using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class amo : MonoBehaviour
{
    public LayerMask LayerDame;
    public float Dame;
    public Vector2 ForceNock;
    public float dir;
    public float Speed;
    public float TimeCC;
    public UnityEvent DameEvent;
    Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    public void On(float d) {

       
        if (rig == null)
        {
            return;
        }
        dir = d;
        rig.velocity = new Vector2(dir * Speed, 0);
        StartCoroutine(Delay(5));
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null)
        {
            Debug.LogError("other is null");
            return;
        }
        // Kiểm tra nếu đối tượng có layer là "map"
        if (LayerDame == (LayerDame | (1 << other.gameObject.layer)))
        {
           
            rig.velocity = Vector2.zero;
            DameEvent.Invoke();
            heath player_heath = other.GetComponent<heath>();
            if (player_heath != null && player_heath.can_dame)
            {

                movement player_move = other.GetComponent<movement>();
                if (player_move != null)
                {
                    player_move.delay_move = true;
                    player_move.rig.velocity = Vector2.zero;
                    float dir = transform.position.x > other.transform.position.x ? 1 : -1;
                    if (player_heath.curren_hp > Dame)
                        player_move.rig.velocity = new Vector2((ForceNock.x - player_heath.anti_nock) * -dir, ForceNock.y - player_heath.anti_nock);
                    else
                    {
                        player_move.rig.velocity = new Vector2((ForceNock.x - player_heath.anti_nock) * -dir, ForceNock.y - player_heath.anti_nock);
                    }
                    StartCoroutine(ResetDelay(player_move, TimeCC - player_heath.anti_nock));
                    player_heath.dame_attack(Dame);
                }
            }
        }
    }
    public void DestroyObject(float time)
    {
        StartCoroutine(Delay(time));
    }
    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private IEnumerator ResetDelay(movement state, float delay)
    {
        yield return new WaitForSeconds(delay);
        state.delay_move = false;
    }
    public void EndDestroyAmo()
    {
        SpriteRenderer amosp;
        amosp = gameObject.GetComponent<SpriteRenderer>();
        if (amosp != null)
        {
            amosp.enabled = false;
        }
    }
}
