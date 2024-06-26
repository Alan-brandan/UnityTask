using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsIconsManager : MonoBehaviour
{
    public Sprite InteractJoystick, CancelJoystick, MovementJoystick,InventoryJoystick, InteractKB, CancelKB, MovementKB,InventoryKB;
    public Image InteractIcon, CancelIcon, RunIcon, MovementIcon,InventoryIcon;

    private bool currentlyUsingJoystick;

    void Update()
    {
        if (currentlyUsingJoystick && Inputmanager.Instance.InputState == Inputmanager.InputType.MouseKeyboard)
        {
            SwitchControlsToKeyboard();
            currentlyUsingJoystick = false;
        }
        else if (!currentlyUsingJoystick && Inputmanager.Instance.InputState == Inputmanager.InputType.Controller)
        {
            SwitchControlsToJoystick();
            currentlyUsingJoystick = true;
        }
    }

    public void SwitchControlsToJoystick()
    {
        ModifyRectSize(InteractIcon,45);
        InteractIcon.sprite = InteractJoystick;
        ModifyRectSize(CancelIcon, 45);
        CancelIcon.sprite = CancelJoystick;
        ModifyRectSize(RunIcon, 45);
        RunIcon.sprite = CancelJoystick;
        ModifyRectSize(MovementIcon, 60);
        MovementIcon.sprite = MovementJoystick;
        ModifyRectSize(InventoryIcon, 60);
        InventoryIcon.sprite = InventoryJoystick;
    }
    public void SwitchControlsToKeyboard()
    {
        ModifyRectSize(InteractIcon, 60);
        InteractIcon.sprite = InteractKB;
        ModifyRectSize(CancelIcon, 75);
        CancelIcon.sprite = CancelKB;
        ModifyRectSize(RunIcon, 75);
        RunIcon.sprite = CancelKB;
        ModifyRectSize(MovementIcon, 90);
        MovementIcon.sprite = MovementKB;
        ModifyRectSize(MovementIcon, 90);
        InventoryIcon.sprite = InventoryKB;
    }

    private void ModifyRectSize(Image img, float newvalue)
    {
        Vector2 sizeDelta = img.rectTransform.sizeDelta;
        sizeDelta.x = sizeDelta.y = newvalue;
        img.rectTransform.sizeDelta = sizeDelta;
    }
}
