using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillCanvas;
    private bool SkillTreeOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleSkill"))
        {
            if (SkillTreeOpen)
            {
                Time.timeScale = 1;
                skillCanvas.alpha = 0;
                skillCanvas.blocksRaycasts = false;
                SkillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillCanvas.alpha = 1;
                skillCanvas.blocksRaycasts = true;
                SkillTreeOpen = true;
            }
        }
    }
}
