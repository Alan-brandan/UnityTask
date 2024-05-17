using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class MerchantShopUi : MonoBehaviour
{
    public GameObject ItemPrefab;

    public List<TradeableItem> stock;

    private void OnEnable()
    {
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;
        print("onenable called");
        PopulateOrUpdateShop();
    }

    private void OnDisable()
    {
        GameManager.Instance.MovementEnabled = true;
        GameManager.Instance.NavigatingMenu = false;
    }

    private void Update()
    {
        if (!CanvasManager.Instance.ConfirmPurchase.activeSelf)
        {
            if (Input.GetKeyDown(Inputmanager.Instance.Run))
            {
                CanvasManager.Instance.merchantStore.SetActive(false);
            }
        }
    }

    public void PopulateOrUpdateShop()
    {
        foreach (TradeableItem item in stock)
        {
            Transform existingItem = FindExistingItem(item);
            GameObject obj;
            if (existingItem != null)
            {
                obj = existingItem.gameObject;
            }
            else
            {
                obj = Instantiate(ItemPrefab, transform);
            }
            print("populate init");

            ItemEntryUi itemEntry = obj.GetComponent<ItemEntryUi>();
            itemEntry.item = item;
            itemEntry.Initialize();

            if (EventSystem.current.firstSelectedGameObject == null)
            {
                Button button = obj.GetComponentInChildren<Button>();
                if (button != null)
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                }
            }
        }
    }

    private Transform FindExistingItem(TradeableItem item)
    {
        foreach (Transform child in transform)
        {
            ItemEntryUi itemEntry = child.GetComponent<ItemEntryUi>();
            if (itemEntry != null && itemEntry.item == item)
            {
                return child;
            }
        }
        return null;
    }
}
