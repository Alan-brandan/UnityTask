using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public PlayerStateManager playerStates;

    private Animator anim;
    private string currentAnimState;
    public string previousAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    #region Assigning animations
    const string IDLE_DOWN = "idleDown";
    const string IDLE_UP = "idleUp";
    const string IDLE_LEFT = "idleLeft";
    const string IDLE_RIGHT = "idleRight";
    const string WALK_DOWN = "walkDown";
    const string WALK_UP = "walkUp";
    const string WALK_LEFT = "walkLeft";
    const string WALK_RIGHT = "walkRight";
    const string RUN_DOWN = "runDown";
    const string RUN_UP = "runUp";
    const string RUN_LEFT = "runLeft";
    const string RUN_RIGHT = "runRight";
    #endregion

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState) return;
        previousAnim = currentAnimState;
        anim.Play(newState);
        currentAnimState = newState;
    }

    void Update()
    {
        if (playerStates != null && anim != null)
        {
            if (playerStates.playerMovement != Vector2.zero)
            {
                if (playerStates.playerRunning)
                {
                    if (playerStates.playerMovement.x != 0)
                    {
                        ChangeAnimationState(playerStates.playerMovement.x > 0 ? RUN_RIGHT : RUN_LEFT);
                    }
                    else if (playerStates.playerMovement.y != 0)
                    {
                        ChangeAnimationState(playerStates.playerMovement.y > 0 ? RUN_UP : RUN_DOWN);
                    }
                }
                else
                {
                    if (playerStates.playerMovement.x != 0)
                    {
                        ChangeAnimationState(playerStates.playerMovement.x > 0 ? WALK_RIGHT : WALK_LEFT);
                    }
                    else if (playerStates.playerMovement.y != 0)
                    {
                        ChangeAnimationState(playerStates.playerMovement.y > 0 ? WALK_UP : WALK_DOWN);
                    }
                }
            }
            else
            {
                switch (previousAnim)
                {
                    case RUN_LEFT:
                    case WALK_LEFT:
                        ChangeAnimationState(IDLE_LEFT);
                        break;
                    case RUN_RIGHT:
                    case WALK_RIGHT:
                        ChangeAnimationState(IDLE_RIGHT);
                        break;
                    case RUN_UP:
                    case WALK_UP:
                        ChangeAnimationState(IDLE_UP);
                        break;
                    case RUN_DOWN:
                    case WALK_DOWN:
                        ChangeAnimationState(IDLE_DOWN);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}