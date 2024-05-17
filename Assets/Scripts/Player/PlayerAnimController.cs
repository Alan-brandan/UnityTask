using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerStateManager playerStates;

    private Animator anim;
    public string currentAnimState;
    public string previousAnim;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerStates = transform.GetComponentInParent<PlayerStateManager>();
    }

    private void Update()
    {
        if (playerStates != null && anim != null)
        {
            string targetAnim = GetTargetAnimation();
            if (targetAnim != currentAnimState)
            {
                ChangeAnimationState(targetAnim);
            }
        }
    }

    private string GetTargetAnimation()
    {
        if (playerStates.playerMovement.magnitude > 0)
        {
            string movementDirection = GetMovementDirection(playerStates.playerMovement);
            return playerStates.playerRunning ? GetRunAnimation(movementDirection) : GetWalkAnimation(movementDirection);
        }
        else
        {
            return GetIdleAnimation(previousAnim);
        }
    }

    private string GetMovementDirection(Vector2 movement)
    {
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            return movement.x > 0 ? "Right" : "Left";
        }
        else
        {
            return movement.y > 0 ? "Up" : "Down";
        }
    }

    private string GetRunAnimation(string direction)
    {
        return "run" + direction;
    }

    private string GetWalkAnimation(string direction)
    {
        return "walk" + direction;
    }

    private string GetIdleAnimation(string previousAnimation)
    {
        switch (previousAnimation)
        {
            case RUN_LEFT:
            case WALK_LEFT:
                return IDLE_LEFT;
            case RUN_RIGHT:
            case WALK_RIGHT:
                return IDLE_RIGHT;
            case RUN_UP:
            case WALK_UP:
                return IDLE_UP;
            case RUN_DOWN:
            case WALK_DOWN:
                return IDLE_DOWN;
            default:
                return IDLE_DOWN; 
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimState != newState)
        {
            anim.Play(newState);
            previousAnim = currentAnimState;
            currentAnimState = newState;
        }
    }
}