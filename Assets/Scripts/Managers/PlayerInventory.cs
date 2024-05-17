using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    public List<TradeableItem> stock;

    public PlayerSkinManager playerskin;

    #region Singleton
    private static PlayerInventory _instance = null;
    public static PlayerInventory Instance { get { return _instance; } }
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

    public void ChangePlayerTop(GameObject newshirt)
    {
        playerskin.ChangeTop(newshirt);
    }
    public void ChangePlayerBottom(GameObject newshirt)
    {
        playerskin.ChangeBottom(newshirt);
    }
}
