using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb; //引用敌人刚体组件
    private Enemy_Movement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }

    public void Knockback(Transform forceTransform,float knockbackForce,float knockbackTime,float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime,stunTime));
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
        Debug.Log("knockback applied.");
    }

    IEnumerator StunTimer(float knockbackTime,float stunTime)//携程
    {
        yield return new WaitForSeconds(knockbackTime);//等待击退时间结束
        rb.velocity = Vector2.zero;//让击退效果结束时有个利落的收尾
        yield return new WaitForSeconds(stunTime);//一直停在这一行直到眩晕结束
        enemy_Movement.ChangeState(EnemyState.idle);
    }



}
