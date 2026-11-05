using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text pointsText; //显示当前拥有的能力点数
    public int availablePoints;




    private void OnEnable()//确保在监听onabilitypointspent
    {
        SkillSlot.onAbilityPointSpent += HandleAbilityPointsSpent;//表示我们正在订阅想要监听它,每当它被调用时我们就要调用handleabilitypointsspent
        SkillSlot.onSkillMaxed += HandleSkillMaxed;
        expManger.OnLevelUp += UpdateAbilityPoints;
    }

    private void OnDisable()//确保在监听onabilitypointspent
    {
        SkillSlot.onAbilityPointSpent -= HandleAbilityPointsSpent;//表示我们取消订阅
        SkillSlot.onSkillMaxed -= HandleSkillMaxed;
        expManger.OnLevelUp -= UpdateAbilityPoints;
    }


    private void Start()
    {
        foreach(SkillSlot slot in skillSlots)//这个循环会遍历数组中的每一项//对于每个技能槽skillslot取名为slot在skillSlots数组中
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));//给按钮加一个监听器作用是如果我们点击它们它们就会告诉特定槽位，它应该尝试升级技能
        }
        UpdateAbilityPoints(0);

    }

    private void CheckAvailablePoints(SkillSlot slot)//检查是否真的有技能点数升级
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }



    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if(availablePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }
    
    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())//检查还未解锁同时可以解锁
            {
                slot.Unlock(); 
            }
        }
    }


    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = " Skill Points: " + availablePoints;
    }
}
