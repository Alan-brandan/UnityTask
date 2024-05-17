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
                CanvasManager.Instance.helppanel.StartHelpPanelCoroutine(false, "", true, false, false);
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
                CanvasManager.Instance.helppanel.StartHideAllCoroutine();
            }
            playerDetected = false;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.canStartDialogues && playerDetected)
        {
            if (Input.GetKeyDown(Inputmanager.Instance.Interact) && !CanvasManager.Instance.merchantOptions.activeSelf)
            {
                if (!GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu && Interactable)
                {
                    showingHelpPanel = false;
                    CanvasManager.Instance.helppanel.StartHideAllCoroutine();
                    StartDialogue();
                    print("detected interact button");
                }
                else if(!GameManager.Instance.NavigatingMenu)
                {
                    ShowNextDialogue();
                }
            }

            if (!GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu && !showingHelpPanel && Interactable)
            {
                CanvasManager.Instance.helppanel.StartHelpPanelCoroutine(false, "", true, false, false);
                showingHelpPanel = true;
            }
        }
    }

    private void StartDialogue()
    {
        print("start dialogue");
        CanvasManager.Instance.helppanel.StartHelpPanelCoroutine(false, "", true, false, false, 0.2f);

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
        if (GameManager.Instance.NavigatingMenu)
            return;
        currentDialogueIndex++;
        ShowDialogue();
    }

    private void EndDialogue()
    {
        if (GameManager.Instance.NavigatingMenu)
            return;

        CanvasManager.Instance.HideDialogueBubble();
        GameManager.Instance.playerInConversation = false;
        if(timesVisited < Dialogues.Length-1)
            timesVisited++;
        OpenShop();
        StartCoroutine(DelayToInteractAgain());
    }

    private IEnumerator DelayToInteractAgain()
    {
        yield return new WaitForSeconds(InteractionCooldown);
        Interactable = true;
    }

    public void OpenShop()
    {
        if (GameManager.Instance.NavigatingMenu)
            return;
        print("open options");
        GameManager.Instance.canStartDialogues = false;
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;
        CanvasManager.Instance.merchantOptions.SetActive(true);
    }
}