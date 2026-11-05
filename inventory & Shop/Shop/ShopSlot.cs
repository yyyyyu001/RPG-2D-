using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ItemSO ItemSO;
    public TMP_Text itemNameText;
    public TMP_Text PriceText;
    public Image itemImage;

    private int price;


    public void Initialize(ItemSO newItemSO,int price)
    {
        //填充正确的名称、价格和物品图片
        ItemSO = newItemSO;
        itemImage.sprite = ItemSO.icon;
        itemNameText.text = ItemSO.itemName;
        this.price = price;
        PriceText.text = price.ToString();
    }
}
