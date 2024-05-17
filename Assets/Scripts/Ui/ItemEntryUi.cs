using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEntryUi : MonoBehaviour
{
    public TradeableItem item;
    public TextMeshProUGUI price;
    public Image icon, type;

    private Button btn;

    public void Initialize()
    {
        btn = GetComponentInChildren<Button>();
        price.text = item.itemCost.ToString();
        icon.sprite = item.itemIcon;
        type.sprite = item.TypeIcon;


        if (PlayerInventory.Instance.stock.Contains(item))
        {
            btn.interactable = false;
            price.color = Color.red;
            price.fontSize = 11;
            price.text = "SOLD OUT";
        }
        else if (TransactionManager.Instance.Gold < item.itemCost)
        {
            btn.interactable = false;
            price.color = Color.red;
        }
        else
        {
            btn.interactable = true;
            price.color = Color.black;
        }
    }

    private void Update()
    {
        if (btn != null && CanvasManager.Instance!=null && item!=null)
        {
            if (EventSystem.current.currentSelectedGameObject == btn.gameObject)
            {
                CanvasManager.Instance.ShopItemInfo.Title.text = item.itemName;
                CanvasManager.Instance.ShopItemInfo.Description.text = item.itemDescription;
            }
        }
    }

    public void OpenConfirmPurchase()
    {
        CanvasManager.Instance.ConfirmPurchase.SetActive(true);
        CanvasManager.Instance.ConfirmPurchase.GetComponent<ConfirmPurchase>().item = item;
    }
}
