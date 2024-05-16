using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> stock;


    #region Singleton
    private static PlayerInventory _instance = null;
    public static PlayerInventory Instance { get { return _instance; } }
    #endregion
    private void Awake()
    {
        #region Singleton initialization
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //Debug.Log("this is the instance", this);
        }
        #endregion

    }

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
