using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpPanelManager : MonoBehaviour
{
    public GameObject panel,Controls,FirstTimeControls;
    public TextMeshProUGUI MessageDisplay;
    private bool currentlyUsingJoystick;

    [Range(0.1f,10)]
    public float FirstTimeControlsLifetime;

    public bool helpanelActive;


    private void Start()
    {
        StartCoroutine(ShowHelpPanel(false,"",false,false,true));
    }

    void Update()
    {
        if(currentlyUsingJoystick && Inputmanager.Instance.InputState == Inputmanager.InputType.MouseKeyboard)
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

    }
    public void SwitchControlsToKeyboard()
    {

    }

    public IEnumerator ShowHelpPanel(bool showMessage, string text, bool showControls, bool showCancel, bool showFirstTimeControls)
    {
        panel.SetActive(true);

        if (showMessage)
        {
            MessageDisplay.text = text;
            MessageDisplay.gameObject.SetActive(true);
        }

        else if (showControls)
        {
            Controls.SetActive(true);
            Controls.transform.GetChild(2).gameObject.SetActive(showCancel);
            Controls.transform.GetChild(3).gameObject.SetActive(showCancel);
        }

        else if (showFirstTimeControls)
        {
            FirstTimeControls.SetActive(true);
            yield return new WaitForSeconds(FirstTimeControlsLifetime);
            StartCoroutine(HideAll());
        }
    }

    public IEnumerator HideAll()
    {
        yield return new WaitForSeconds(0.1f);
        panel.SetActive(false);

        MessageDisplay.gameObject.SetActive(false);
        Controls.SetActive(false);
        FirstTimeControls.SetActive(false);
    }
}
