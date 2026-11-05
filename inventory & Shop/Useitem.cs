using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useitem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        if(itemSO.currentHealth > 0)
            StateManger.instance.Updatehealth(itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StateManger.instance.UpdateMaxhealth(itemSO.maxHealth);
        if (itemSO.speed > 0)
            StateManger.instance.UpdateSpeed(itemSO.speed);
        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

    }

    private IEnumerator EffectTimer(ItemSO itemSO,float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.currentHealth > 0)
            StateManger.instance.Updatehealth(-itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StateManger.instance.UpdateMaxhealth(-itemSO.maxHealth);
        if (itemSO.speed > 0)
            StateManger.instance.UpdateSpeed(-itemSO.speed);
     
    } 
}
