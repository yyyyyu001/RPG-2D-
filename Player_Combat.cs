using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1;
    public float knockbackForce = 50;
    public float knockbackTime = .15f;
    public float stunTime = .3f;
    public LayerMask enemyLayer;
    public int damage = 2;

    public Animator anim;

    public float cooldown = 2;
    private float timer;

    public void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;//»√timeµπº∆ ±
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = StateManger.instance.cooldown;
        }
    }

    public void DealDamage()
    {

        Collider2D[] enemise = Physics2D.OverlapCircleAll(attackPoint.position, StateManger.instance.weaponRange, enemyLayer);
        if (enemise.Length > 0)
        {
            enemise[0].GetComponent<Enemy_Health>().ChangeHealth(-StateManger.instance.damage);
            enemise[0].GetComponent<Enemy_Knockback>().Knockback(transform, StateManger.instance.knockbackForce, StateManger.instance.knockbackTime, StateManger.instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StateManger.instance.weaponRange);
        
    }
}
