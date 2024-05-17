using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour,IPickable
{
    [Range(1,10)]
    public int value = 1;

    public void Pickup()
    {
        TransactionManager.Instance.AddGold(value);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((GameManager.Instance.playerLayer & (1 << collision.gameObject.layer)) > 0)
        {
            Pickup();
        }
    }
}
