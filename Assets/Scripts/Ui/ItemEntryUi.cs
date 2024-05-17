using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntryUi : MonoBehaviour
{
    public TradeableItem item;
    public TextMeshProUGUI title, description, price;
    public Image icon, type;

    public void Initialize()
    {
        if(title!=null)
            title.text = item.itemName;
        if(description!=null)
            description.text = item.itemDescription;
        price.text = item.itemCost.ToString();
        icon.sprite = item.itemIcon;

        if (TransactionManager.Instance.Gold < item.itemCost)
        {
            GetComponentInChildren<Button>().interactable = false;
            price.color = Color.red;
        }
    }
}
