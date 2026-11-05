using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;


    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockedBack;
    public bool isShooting;

    public Player_Combat player_Combat;

    private void Update()//输入反馈最快的方式
    {
        if (Input.GetButtonDown("slash") && player_Combat.enabled == true )//检查是否按下了攻击键//记得修复
        {
            player_Combat.Attack();
        }
       
    }

    // Fixed Update is called 50x frame
    void FixedUpdate()//fixedupdate 稳定且规律
    {
        if(isShooting == true)
        {
            rb.velocity = Vector2.zero;
        }
        
       else if (isKnockedBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
               horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }


            //mathf.abs作用就是将发送给动画控制器的数值转换为正数（绝对值函数）；
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StateManger.instance.speed;
        }
    }


    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force,float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }
    IEnumerator KnockbackCounter(float stunTime)//枚举器击退计数器
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
 }
