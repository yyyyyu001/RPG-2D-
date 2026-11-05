using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemso;
    public SpriteRenderer sr;
    public Animator anim;

    public bool canBePickedUp = true;
    public int quantity;
    public static event Action<ItemSO, int> OnitemLooted;

    private void OnValidate()
    {
        if (itemso == null)
            return;
        UpdateAppearance();
    }

    public void Initialize(ItemSO itemSO,int quantity)
    {
        this.itemso = itemSO;
        this.quantity = quantity;
        canBePickedUp = false;
        UpdateAppearance();
    }
   private void UpdateAppearance()
    {
        sr.sprite = itemso.icon;
        this.name = itemso.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp == true)
        {
            anim.Play("LootPick");
            OnitemLooted?.Invoke(itemso, quantity);//？是否有监听事件存在，如果有就会调用这个invoke里的事件；
            Destroy(gameObject, .8f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }

    }
}
