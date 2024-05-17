using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopOptions : MonoBehaviour
{
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
        GameManager.Instance.MovementEnabled = true;
        GameManager.Instance.NavigatingMenu = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Inputmanager.Instance.Run))
        {
            CanvasManager.Instance.merchantOptions.SetActive(false);
        }
    }
}
