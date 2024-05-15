using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionManager : MonoBehaviour
{
    public int Gold;

    #region Singleton
    private static TransactionManager _instance = null;
    public static TransactionManager Instance { get { return _instance; } }
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

    public void AddGold(int amount)
    {
        Gold += amount;
    }
    public void SubstractGold(int amount)
    {
        Gold -= amount;
    }
}
