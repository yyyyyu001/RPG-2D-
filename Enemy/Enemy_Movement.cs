using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1; //设定初始朝向为左-1；
    private EnemyState enemyState ;//追踪当前状态
    private  Rigidbody2D rb; //创建一个公有的刚体引用叫rb
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>(); //这样刚体就会自动从组件里寻找rigi这个对象
        anim = GetComponent<Animator>();//它会为动画控制器找到游戏对象并填充那个槽位
        ChangeState(EnemyState.idle);//一开始将状态改为闲置
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForplayer();

            if (attackCooldownTimer > 0) //冷却效果还在生效
            {
                attackCooldownTimer -= Time.deltaTime;//让他倒计时
            }
            if (enemyState == EnemyState.walking)//检查处于哪个状态
            {
                Chase();
            }
            else if (enemyState == EnemyState.attacking)
            {
                //处理所有攻击相关的逻辑
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Chase()//只负责转向和实际的追逐
    {
         if (player.position.x > transform.position.x && facingDirection == -1 ||
               player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();//调用翻转函数
        }
        Vector2 direction = (player.position - transform.position).normalized; // 让它的大小（模长）归一化为1，意味着它的x和y的分量都将在0到1之间
        rb.velocity = direction * speed;
    }




               void Flip()//创建翻转函数
    {
        facingDirection *= -1;//总会变成与之前相反的值
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForplayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0) //检测视野内是否有玩家
        {
            player = hits[0].transform;
            //检查该玩家是否在攻击范围内，以及我们的冷却实际是否确实就绪
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;//将攻击冷却计时器设为实际攻击冷却时间//重置冷却时间进入攻击状态
                ChangeState(EnemyState.attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.attacking)
            {
                ChangeState(EnemyState.walking);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.idle);
        }

           
        
       
    }
  
   public  void ChangeState(EnemyState newState)
    {
        //第一部分是退出当前动画
        if (enemyState == EnemyState.idle)
            anim.SetBool("isldie", false);
        else if (enemyState == EnemyState.walking)
            anim.SetBool("iswalking", false);
        else if (enemyState == EnemyState.attacking)
            anim.SetBool("isattacking", false);
        //更新当前状态
        enemyState = newState;
        //更新新的动画
        if (enemyState == EnemyState.idle)
            anim.SetBool("isldie", true);
        else if (enemyState == EnemyState.walking)
            anim.SetBool("iswalking", true);
        else if (enemyState == EnemyState.attacking)
            anim.SetBool("isattacking", true);
    }
}


public enum EnemyState
{ 
     idle,
     walking,
     attacking,
     Knockback
} //创建自己的变量类型;