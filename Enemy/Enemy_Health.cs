using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int ExpReward = 3;

    public delegate void MonsterDefeated(int exp);//创建一个事件委托
    public static event MonsterDefeated OnMonsterDefeated;//创建一个事件

    public int currentHealth;
    public int maxHealth;

    private void Start()//初始敌人血量
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Destroy(gameObject);
            OnMonsterDefeated(ExpReward);
           
        }
    }

}
