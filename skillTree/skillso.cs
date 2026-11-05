using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="NewSkill",menuName = "SkillTree/Skill")]
public class skillso : ScriptableObject
{
    public string skillName;
    public int maxLevel;
    public Sprite skillicon;//在技能槽上显示的图标
}
