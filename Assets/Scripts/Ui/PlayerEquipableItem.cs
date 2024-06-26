using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipableItem : MonoBehaviour
{
    public TradeableItem item;
    public TextMeshProUGUI title;
    public Image icon;

    public void Initialize()
    {
        title.text = item.itemName;
        icon.sprite = item.itemIcon;
    }

    public void EquipToPlayer()
    {
        if (item.type == 0)
        {
            PlayerInventory.Instance.ChangePlayerTop(item.prefab);
        }
        else
        {
            PlayerInventory.Instance.ChangePlayerBottom(item.prefab);
        }
    }
}
