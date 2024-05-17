using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TradeableItem")]
public class TradeableItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public Sprite TypeIcon;
    public int type = 0; //0 shirt, 1 bottoms
    public int itemCost;
    public GameObject prefab;
}
