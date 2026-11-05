using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat combat;

    private void OnEnable()
    {
        SkillSlot.onAbilityPointSpent += HandleAbilityPointSpent;
    }
    private void OnDisnable()
    {
        SkillSlot.onAbilityPointSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillso.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StateManger.instance.UpdateMaxhealth(1);
                break;

            case "Sword Slash":
                    combat.enabled = true;
                break;

            default:
                Debug.LogWarning("Unknown skill: " + skillName);
                break;
        }
    }


}
