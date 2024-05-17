using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelManager : MonoBehaviour
{
    public GameObject panel,Controls,FirstTimeControls;
    public TextMeshProUGUI MessageDisplay;

    [Range(0.1f,10)]
    public float FirstTimeControlsLifetime;

    public bool helpanelActive;

    private Coroutine helpPanelCoroutine;
    private Coroutine hideAllCoroutine;

    private void Start()
    {
        StartCoroutine(ShowHelpPanel(false,"",false,false,true));
    }

    public void StartHelpPanelCoroutine(bool showMessage, string text, bool showControls, bool showCancel, bool showFirstTimeControls, float duration = 0.5f)
    {
        if (helpPanelCoroutine != null)
        {
            StopCoroutine(helpPanelCoroutine);
        }
        helpPanelCoroutine = StartCoroutine(ShowHelpPanel(showMessage, text, showControls, showCancel, showFirstTimeControls, duration));
    }

    public void StartHideAllCoroutine()
    {
        if (hideAllCoroutine != null)
        {
            StopCoroutine(hideAllCoroutine);
        }
        hideAllCoroutine = StartCoroutine(HideAll());
    }

    private IEnumerator ShowHelpPanel(bool showMessage, string text, bool showControls, bool showCancel, bool showFirstTimeControls, float duration=0.5f)
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
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            foreach (Transform child in transform)
            {
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    Color color = image.color;
                    color.a = Mathf.Lerp(0f, 0.8f, t);
                    image.color = color;
                }

                TextMeshProUGUI _text = child.GetComponent<TextMeshProUGUI>();
                if (_text != null)
                {
                    _text.overrideColorTags = true;
                    Color32 color = _text.color;
                    color.a = (byte)Mathf.Lerp(0f, 255f, t);
                    _text.color = color;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform child in transform)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = 0.8f;
                image.color = color;
            }

            TextMeshProUGUI _text = child.GetComponent<TextMeshProUGUI>();
            if (_text != null)
            {
                _text.overrideColorTags = true;
                Color32 color = _text.color;
                color.a = 255;
                _text.color = color;
            }
        }

        if (showFirstTimeControls)
        {
            yield return new WaitForSeconds(FirstTimeControlsLifetime);
            StartCoroutine(HideAll());
        }
    }

    private IEnumerator HideAll()
    {
        float duration =  0.5f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            foreach (Transform child in transform)
            {
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    Color color = image.color;
                    color.a = Mathf.Lerp(0.8f, 0f, t);
                    image.color = color;
                }

                TextMeshProUGUI _text = child.GetComponent<TextMeshProUGUI>();
                if (_text != null)
                {
                    Color32 color = _text.color;
                    color.a = (byte)Mathf.Lerp(255f, 0f, t);
                    _text.color = color;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform child in transform)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = 0f;
                image.color = color;
            }

            TextMeshProUGUI _text = child.GetComponent<TextMeshProUGUI>();
            if (_text != null)
            {
                Color32 color = _text.color;
                color.a = 0;
                _text.color = color;
            }
        }

        panel.SetActive(false);
        MessageDisplay.gameObject.SetActive(false);
        Controls.SetActive(false);
        FirstTimeControls.SetActive(false);

    }
}
