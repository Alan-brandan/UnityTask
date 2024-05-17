using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private int timesVisited=0;
    private int currentDialogueIndex=0;
    public Dialogue[] Dialogues;

    private bool Interactable = true;

    public float InteractionCooldown;

    private bool showingHelpPanel = false;
    private bool playerDetected = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((GameManager.Instance.playerLayer & (1 << collision.gameObject.layer)) > 0)
        {
            if (!showingHelpPanel && Interactable)
            {
                StartCoroutine(CanvasManager.Instance.helppanel.ShowHelpPanel(false, "", true, false, false));
                showingHelpPanel = true;
            }
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((GameManager.Instance.playerLayer & (1 << collision.gameObject.layer)) > 0)
        {
            if (showingHelpPanel)
            {
                showingHelpPanel = false;
                StartCoroutine(CanvasManager.Instance.helppanel.HideAll());
            }
            playerDetected = false;
        }
    }

    private void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(Inputmanager.Instance.Interact))
            {
                if (!GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu)
                {
                    showingHelpPanel = false;
                    StartCoroutine(CanvasManager.Instance.helppanel.HideAll());
                    StartDialogue();
                }
                else
                {
                    ShowNextDialogue();
                }
            }

            if (!GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu && !showingHelpPanel && Interactable)
            {
                StartCoroutine(CanvasManager.Instance.helppanel.ShowHelpPanel(false, "", true, false, false));
                showingHelpPanel = true;
            }
        }
    }

    private void StartDialogue()
    {
        Interactable = false;
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.playerInConversation = true;

        currentDialogueIndex = 0; 
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentDialogueIndex < Dialogues[timesVisited].lines.Length)
        {
            CanvasManager.Instance.ShowDialogueBubble(Dialogues[timesVisited].lines[currentDialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        ShowDialogue();
    }

    private void EndDialogue()
    {
        CanvasManager.Instance.HideDialogueBubble();
        GameManager.Instance.playerInConversation = false;
        if(timesVisited < Dialogues.Length-1)
            timesVisited++;
        StartCoroutine(DelayToInteractAgain());
        if(!GameManager.Instance.NavigatingMenu)
            OpenShop();
    }

    private IEnumerator DelayToInteractAgain()
    {
        yield return new WaitForSeconds(InteractionCooldown);
        Interactable = true;
    }

    public void OpenShop()
    {
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;
        CanvasManager.Instance.merchantOptions.SetActive(true);
    }
}