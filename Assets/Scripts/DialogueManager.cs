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

    public Vector3 dialoguepos;
    bool justchatted = false;

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
        if (playerDetected)
        {
            if(!GameManager.Instance.NavigatingMenu && justchatted)
            {
                StartCoroutine(DelayToInteractAgain());
            }

            if (!justchatted && Input.GetKeyDown(Inputmanager.Instance.Interact) && !CanvasManager.Instance.merchantOptions.activeSelf)
            {
                if (!GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu && Interactable)
                {
                    showingHelpPanel = false;
                    CanvasManager.Instance.helppanel.StartHideAllCoroutine();
                    StartDialogue();
                }
                else if(!GameManager.Instance.NavigatingMenu)
                {
                    ShowNextDialogue();
                }
            }

            if (!justchatted && !GameManager.Instance.playerInConversation && !GameManager.Instance.NavigatingMenu && !showingHelpPanel && Interactable)
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
            CanvasManager.Instance.ShowDialogueBubble(Dialogues[timesVisited].lines[currentDialogueIndex],dialoguepos);
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
        justchatted = true;
    }

    private IEnumerator DelayToInteractAgain()
    {
        yield return new WaitForSeconds(InteractionCooldown);
        Interactable = true;
        justchatted = false;

    }

    public void OpenShop()
    {
        GameManager.Instance.MovementEnabled = false;
        GameManager.Instance.NavigatingMenu = true;
        CanvasManager.Instance.merchantOptions.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dialoguepos + transform.position, .1f);
    }
}