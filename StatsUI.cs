using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsopen = false;

    private void Start()
    {
        updateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
            if (statsopen)
            {
                Time.timeScale = 1;
                updateAllStats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsopen = false;
            }
            else
            {
                Time.timeScale = 0;
                updateAllStats();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsopen = true;
            }

    }

    public void updateDamage()//创建一个公有的void方法，叫updateDamage
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StateManger.instance.damage;
    }
    public void updateSpeed()//创建一个公有的void方法，叫updateSpeed
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StateManger.instance.speed;
    }
    public void updateAllStats()
    {
        updateDamage();
        updateSpeed();
    }
}
