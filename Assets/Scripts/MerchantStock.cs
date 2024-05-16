using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantStock : MonoBehaviour
{
    public List<InventoryItem> stock;

    public void PurchaseItem(InventoryItem item)
    {
        if (stock.Contains(item))
            stock[stock.IndexOf(item)].quantity++;
        else
            stock.Add(item);
    }

    public void SellItem(int index)
    {
        if (stock[index].quantity > 1)
            stock[index].quantity--;
        else
            stock.RemoveAt(index);
    }
}
