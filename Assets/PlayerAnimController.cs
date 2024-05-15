using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public PlayerStateManager playerStates;

    private Animator anim;
    private string currentanimState;
    private string previousani;

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
    const string RUN_lEFT = "runLeft";
    const string RUN_RIGHT = "runRight";


    public void ChangeAnimationState(string newState)
    {
        if (currentanimState == newState) return;
        previousani = currentanimState;
        anim.Play(newState);
        currentanimState = newState;
    }
    #endregion

    void Update()
    {
       if(playerStates!=null && anim != null)
       {
            if (playerStates.playerRunning)
            {
                if (playerStates.playerMovement.x == 1 || playerStates.playerMovement.x == -1)
                {
                    if (playerStates.playerMovement.x == -1)
                        ChangeAnimationState(RUN_lEFT);
                    if (playerStates.playerMovement.x == 1)
                        ChangeAnimationState(RUN_RIGHT);
                }

                else if (playerStates.playerMovement.y == -1)
                    ChangeAnimationState(RUN_DOWN);
                else if (playerStates.playerMovement.y == 1)
                    ChangeAnimationState(RUN_UP);
            }
            else
            {
                if(playerStates.playerMovement.x == 1 || playerStates.playerMovement.x == -1)
                {
                    if (playerStates.playerMovement.x == -1)
                        ChangeAnimationState(WALK_LEFT);
                    if (playerStates.playerMovement.x == 1)
                        ChangeAnimationState(WALK_RIGHT);
                }

                else if (playerStates.playerMovement.y == -1)
                    ChangeAnimationState(WALK_DOWN);
                else if (playerStates.playerMovement.y == 1)
                    ChangeAnimationState(WALK_UP);
                if(playerStates.playerMovement.x == 1 || playerStates.playerMovement.x == -1 || (playerStates.playerMovement.y == 1 || playerStates.playerMovement.y == -1)){
                    anim.speed = 1;
                }
                else
                {
                    anim.speed = 0;
                }
            }
        }
    }
}
