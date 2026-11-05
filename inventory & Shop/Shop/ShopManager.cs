using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItems> shopItems;//List
    [SerializeField] private ShopSlot[] shopSlots;//Array

    private void Start()
    {
        PopulateShopItems();
    }
    public void PopulateShopItems()
    {
        for (int i = 0; i < shopItems.Count && i< shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemso, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }
        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

}



[System.Serializable]//访问system命名空间，并允许我们序列化这个类；
public class ShopItems
{
    public ItemSO itemso;
    public int price;
}
