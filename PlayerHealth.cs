using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
   
    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()//`Start`只执行一次，不是每次血量变化都执行，血量变化靠 `ChangeHealth` 更新文字。
    {
        healthText.text = "HP: " + StateManger.instance.currentHealth + "/ " + StateManger.instance.maxHealth;
        // 游戏启动时初始化血量UI，把当前血量、最大血量拼接成"HP: 100/100"格式显示在文本上
    }



    public void ChangeHealth(int amount)
    {
        StateManger.instance.currentHealth += amount;//当前血量加上传入参数amount等于当前血量;
        healthTextAnim.Play("TextUpdate");

        healthText.text = "HP: " + StateManger.instance.currentHealth + "/ " + StateManger.instance.maxHealth;
        if (StateManger.instance.currentHealth <= 0)//判断当前血量，如果血量小于或等于0玩家直接消失;
        {
            gameObject.SetActive(false);
        }
    }
}
