using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject merchantStore;
    public GameObject merchantOptions;

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

    public void ShowDialogueBubble(string text)
    {
        dialogueBubble.GetComponentInChildren<TextMeshProUGUI>().text = text;
        if(!dialogueBubble.activeSelf)
            dialogueBubble.SetActive(true);
    }
    public void HideDialogueBubble()
    {
        if (dialogueBubble.activeSelf)
            dialogueBubble.SetActive(false);
    }
}
