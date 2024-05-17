using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEntryUi : MonoBehaviour
{
    public TradeableItem item;
    public TextMeshProUGUI title, description, price;
    public Image icon, type;

    private Button btn;

    public void Initialize()
    {
        btn = GetComponentInChildren<Button>();

        if (title!=null)
            title.text = item.itemName;
        if(description!=null)
            description.text = item.itemDescription;
        price.text = item.itemCost.ToString();
        icon.sprite = item.itemIcon;

        if (TransactionManager.Instance.Gold < item.itemCost)
        {
            btn.interactable = false;
            price.color = Color.red;
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
