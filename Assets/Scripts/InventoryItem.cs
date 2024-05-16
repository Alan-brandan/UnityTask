using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public TradeableItem item;
    [Range(1, 99)]
    public int quantity = 1;
}
