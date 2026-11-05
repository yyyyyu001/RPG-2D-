using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public Useitem useitem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform player;

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }
    private void OnEnable()
    {
        Loot.OnitemLooted += Additem;
    }
    private void OnDisable()
    {
        Loot.OnitemLooted -= Additem;
    }


    public void Additem(ItemSO itemso,int quantity)
    {
        if (itemso.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }

        foreach (var slot in itemSlots)
        {
            if(slot.itemso == itemso && slot.quantity < itemso.stackSize)//如果格子的脚本化对象与我们刚捡起的物品匹配，并且格子里的数量小于该物品的最大堆叠数量
            {
                int availableSpace = itemso.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();

                if (quantity <= 0)
                    return;
            }
        }


            foreach (var slot in itemSlots)
            {
                if (slot.itemso == null)
                {
                int amounToAdd = Mathf.Min(itemso.stackSize,quantity);
                    slot.itemso = itemso;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
            }

        if (quantity > 0)
            DropLoot(itemso, quantity);
        
    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemso, 1);
        slot.quantity--;
        if(slot.quantity <= 0)
        {
            slot.itemso = null;
        }
        slot.UpdateUI();
    }
    private void DropLoot(ItemSO itemSO,int qualitaty)
    {
      Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO,qualitaty);
    }

    public void Useitem(InventorySlot slot)
    {
        if(slot.itemso != null && slot.quantity > 0)
        {
            useitem.ApplyItemEffects(slot.itemso);

            slot.quantity--;
            if(slot.quantity <= 0)
            {
                slot.itemso = null;
            }
            slot.UpdateUI();
        }
    }


}
