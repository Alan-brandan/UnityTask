using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmPurchase : MonoBehaviour
{
    public TradeableItem item;

    public void PurchaseItem()
    {
        TransactionManager.Instance.SubstractGold(item.itemCost);
        PlayerInventory.Instance.stock.Add(item);
        CanvasManager.Instance.merchantStore.GetComponentInChildren<MerchantShopUi>().PopulateOrUpdateShop();
    }

    private void OnEnable()
    {
        bool firstSelected = false;
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Button>(out var button))
            {
                if (!firstSelected)
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                    firstSelected = true;
                }
            }
        }
    }

    private void OnDisable()
    {
        CanvasManager.Instance.merchantStore.GetComponentInChildren<MerchantShopUi>().PopulateOrUpdateShop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Inputmanager.Instance.Run))
        {
            CanvasManager.Instance.ConfirmPurchase.SetActive(false);
        }
    }
}
