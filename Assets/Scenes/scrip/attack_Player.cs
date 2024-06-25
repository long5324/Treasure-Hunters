using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_Player : MonoBehaviour
{

    public float speed_attack;
    public float time_end_attack;
    public groudcheck check_air_attack;
    bool delayattack;
    Coroutine comboct;
    public bool is_attack {set;get;}

    public GameObject skill_effect;
     Animator animation_skill;

    Animator animator;
    movement player_move;
    int attack_combo = 1;
    int attack_bombo_air = 1;
    void Start()
    {
        animation_skill= skill_effect.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        player_move = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !delayattack)
        {

            if (player_move.check_ground.is_ground)
            {
                is_attack = true;
                animator.SetTrigger("attack");
            }
            else
            {
                if (!check_air_attack.is_ground && attack_bombo_air <= 2)
                {
                    is_attack = true;
                    animator.SetTrigger("attackair");

                }
            }

        }
        if (attack_bombo_air == 2) { 
            if (player_move.check_ground.is_ground)
            {

                attack_bombo_air = 1;
                animator.SetInteger("comboair", attack_bombo_air);
            }
         }
    }

    IEnumerator waitattackcombo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(attack_combo >1) {
            attack_combo = 1;
            
        }
    }

    IEnumerator waitattack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        delayattack = false;
    }
    public void setonattack()
    {
        delayattack = true;
        StartCoroutine(waitattack(speed_attack));
        animator.SetInteger("combo", attack_combo);
        if (attack_combo < 3)
        {

            attack_combo++;
            

        }
        else if (attack_combo == 3)
        {
            attack_combo = 1;

        }
        if (comboct != null)
            StopCoroutine(comboct);
        if (attack_combo > 1)
        {

            comboct = StartCoroutine(waitattackcombo(time_end_attack));
        }
       
        
        player_move.can_movement = false;
        player_move.rig.velocity = new Vector2(0, player_move.rig.velocity.y);
    }
    public void setonattackair()
    {
        player_move.set_over_speed(3);
        bounceplayer();
        if (attack_bombo_air < 2)
            attack_bombo_air++;
        animator.SetInteger("comboair", attack_bombo_air);
       
    }
    public void setendattackair()
    {
        player_move.end_over_speed();

    }
    public void bounceplayer()
    {
       player_move.rig.velocity = new Vector2( player_move.rig.velocity.x, 5);
    }

    public void setendattack()
    {
        is_attack = false;
        player_move.can_movement = true;
    }
    public void setefskill()
    {
        Debug.Log(attack_combo);
        skill_effect.SetActive(true);
        animation_skill.SetInteger("attack", attack_combo > 1?attack_combo-1:1);
       
    }
}