using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool MovementEnabled,InputEnabled,playerInConversation,NavigatingMenu,canStartDialogues,inventoryopen;

    public LayerMask playerLayer;


    #region Singleton
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }
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
}
