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

    private void Start()
    {
        PopulateShop();
    }

    private void OnEnable()
    {
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;


    }

    private void OnDisable()
    {
        GameManager.Instance.MovementEnabled = true;
        GameManager.Instance.NavigatingMenu = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Inputmanager.Instance.Run))
        {
            CanvasManager.Instance.merchantStore.SetActive(false);
        }
    }

    public void PopulateShop()
    {
        bool selectedfirst = false;
        foreach (TradeableItem item in stock)
        {
            GameObject obj = Instantiate(ItemPrefab, transform);
            obj.GetComponent<ItemEntryUi>().item = item;
            obj.GetComponent<ItemEntryUi>().Initialize();
            if (!selectedfirst)
            {
                EventSystem.current.SetSelectedGameObject(obj.GetComponentInChildren<Button>().gameObject);
                selectedfirst = true;
            }
        }
    }

    public void PurchaseItem(TradeableItem item)
    {
        
    }
}
