using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntryUi : MonoBehaviour
{
    public InventoryItem itemInfo;
    public TextMeshProUGUI title, description, price;
    public Image icon, type;

    private void OnEnable()
    {
        if (itemInfo != null)
        {
            SetInfo();
        }
    }

    public void SetInfo()
    {
        if(title!=null)
            title.text = itemInfo.item.itemName;
        if(description!=null)
            description.text = itemInfo.item.itemDescription;
        price.text = itemInfo.item.itemCost.ToString();
        icon.sprite = itemInfo.item.itemIcon;
    }
}
