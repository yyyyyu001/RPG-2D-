using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;
    public skillso skillso;//技能槽关联到一个特定的可脚本化对象

    public int currentLevel;
    public bool isUnlocked;


    public Image skillicon;//让可脚本化对象能够控制每个技能槽外观
    public Button skillButton;
    public TMP_Text skillLevelText;

    public static event Action<SkillSlot> onAbilityPointSpent;
    public static event Action<SkillSlot> onSkillMaxed;

    private void OnValidate()//每当你修改脚本变量时，此方法就会运行
    {
        if (skillso != null && skillLevelText != null)//简单防御性检查，确保运行代码前已经分配了可脚本化对象和技能等级文本
        {
            
            UpdateUI();
        }
    }



    public void TryUpgradeSkill()
    {
        if(isUnlocked && currentLevel < skillso.maxLevel)
        {
            currentLevel++;
            onAbilityPointSpent?.Invoke(this);//将这个特定的技能槽传递给所有正在监听的脚本
            if(currentLevel >= skillso.maxLevel)
            {
                onSkillMaxed?.Invoke(this);
            }
            UpdateUI();
        }
    }


    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillso.maxLevel)
            {
                return false;
            }
        }
        return true;
    }



    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        skillicon.sprite = skillso.skillicon;//将槽位上的精灵替换为可脚本化对象分配的精灵

        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillso.maxLevel.ToString();
            skillicon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillicon.color = Color.gray;
        }
    }
}
