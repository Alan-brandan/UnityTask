using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TradeableItem")]
public class TradeableItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemCost;
}
