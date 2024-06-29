using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iten_spaw : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng có layer là "map"
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
           attack_Player player_machine = other.gameObject.GetComponent<attack_Player>();
            if(player_machine != null )
            {
                if (player_machine.has_sword)
                    return;
                player_machine.has_sword = true;
                Animator player_anim = player_machine.GetComponent<Animator>();
                player_anim.SetBool("has_sword", true);
                GameObject s= GameObject.Find("sword_throw");
                s.GetComponent<Collider2D>().enabled = true;
                s.SetActive(false);
                Destroy(gameObject);
            }
        }

    }


}
