using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject merchantStore;
    public GameObject merchantOptions;
    public GameObject ConfirmPurchase;
    public ItemInfoPanelUi ShopItemInfo;
    public GameObject PlayerInventory;

    public TextMeshProUGUI MoneyValue;
    public GameObject dialogueBubble;
    public HelpPanelManager helppanel;

    #region Singleton
    private static CanvasManager _instance = null;
    public static CanvasManager Instance { get { return _instance; } }
    #endregion
    private void Awake()
    {
        #region Singleton initialization
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //Debug.Log("this is the instance", this);
        }
        #endregion

    }

    private void Update()
    {
        if (Input.GetKeyDown(Inputmanager.Instance.Inventory))
        {
            if (!GameManager.Instance.playerInConversation && !GameManager.Instance.inventoryopen)
            {
                PlayerInventory.SetActive(true);
                GameManager.Instance.inventoryopen = true;
            }
            else if (GameManager.Instance.inventoryopen)
            {
                PlayerInventory.SetActive(false);
                GameManager.Instance.inventoryopen = false;
            }
        }
    }



    public void ShowDialogueBubble(string text,Vector3 worldpos)
    {
        dialogueBubble.GetComponentInChildren<TextMeshProUGUI>().text = text;

        
        dialogueBubble.SetActive(true);

    }
    public void HideDialogueBubble()
    {
        if (dialogueBubble.activeSelf)
            dialogueBubble.SetActive(false);
    }
}
