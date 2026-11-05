using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour,IPointerClickHandler
{
    public ItemSO itemso;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    private inventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponentInParent<inventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)//如果我们左键并点击了一个格子并且那有物品
            {
                if (itemso.currentHealth > 0 && StateManger.instance.currentHealth >= StateManger.instance.maxHealth)
                    return;
                inventoryManager.Useitem(this);
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if(itemso != null)
        {
            itemImage.sprite = itemso.icon;
            itemImage.gameObject.SetActive(true);//是否应该显示图像来关闭这个sprite
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
       
    }
}
