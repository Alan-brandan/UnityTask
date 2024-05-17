using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventoryUi : MonoBehaviour
{
    public GameObject ItemPrefab;

    private Dictionary<TradeableItem, GameObject> instantiatedItems = new Dictionary<TradeableItem, GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;
        GameManager.Instance.inventoryopen = true;
        PopulateOrUpdateShop();
    }

    private void OnDisable()
    {
        GameManager.Instance.MovementEnabled = true;
        GameManager.Instance.NavigatingMenu = false;
        GameManager.Instance.inventoryopen = false;
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
        foreach (TradeableItem item in PlayerInventory.Instance.stock)
        {
            GameObject obj;
            if (instantiatedItems.ContainsKey(item))
            {
                obj = instantiatedItems[item];
            }
            else
            {
                obj = Instantiate(ItemPrefab, transform);
                instantiatedItems.Add(item, obj);
            }

            PlayerEquipableItem itemEntry = obj.GetComponent<PlayerEquipableItem>();
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
        List<TradeableItem> itemsToRemove = new List<TradeableItem>();
        foreach (var pair in instantiatedItems)
        {
            if (!PlayerInventory.Instance.stock.Contains(pair.Key))
            {
                Destroy(pair.Value);
                itemsToRemove.Add(pair.Key);
            }
        }
        foreach (var itemToRemove in itemsToRemove)
        {
            instantiatedItems.Remove(itemToRemove);
        }
    }
}
