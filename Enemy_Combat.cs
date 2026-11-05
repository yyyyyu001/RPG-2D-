using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask playerLayer;//为检查玩家是否在攻击范围内，将使用player层级遮罩

    

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position,weaponRange, playerLayer);    //创建collider2d数组命名为hits，本质上会生成一个列表包含属于攻击范围内所有属于player层级的物体
        //Physics2D.OverlapCircleAll() 这个函数会在武器周围内创建一个圆形区域，这个原点在攻击点的位置，半径就是我们设定的武器攻击范围，最后寻找属于player层级的物体
        Debug.Log("Attacking Player Now!");
        if(hits.Length > 0)//如果我们的数组中有超过0个元素，就说明我们找到了一个玩家
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<NewBehaviourScript>().Knockback(transform, knockbackForce,stunTime);
        }
    }
}
