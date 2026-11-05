using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class expManger : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowMultiplier = 1.2f;//意味着每次升级所需经验会增加百分之二十;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;//监听事件传入一个整数命名为onlevelup


    private void Start()
    {
        updateUI();
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()//取消订阅该事件监听，
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)//创建一个公共的void方法，叫“获取经验”,它接收一个整数参数
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUP();
        }
        updateUI();
    }

    private void LevelUP()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowMultiplier);
        OnLevelUp?.Invoke(1);
    } 

    public void updateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;

    }
}
