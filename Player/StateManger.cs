using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateManger : MonoBehaviour
{

    public TMP_Text healthText;//每当状态值本身改变时，我们就能实际改变屏幕上显示的最大生命值;
    public StatsUI statsUI;
    public static StateManger instance;//静态数值管理器实例,通过设为静态就可以确保这个变量只有一个副本;
    [Header("Combat States")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public float cooldown;

    [Header("Movement States")]
    public float speed;

    [Header("Health States")]
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMaxhealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }

    public void Updatehealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }

    public void UpdateSpeed(int amount)
    {
       speed += amount;
        statsUI.updateAllStats();
    }
}

