using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerStateManager))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 0;
    public float runSpeed = 0;

    private float currentSpeed = 0;

    private int lookingDirection = 0; // -1 left 0 none 1 right
    private float xMoveDirection = 0;
    private float yMoveDirection = 0;

    private Vector2 finalMovingDirection;
    private Rigidbody2D _rb;
    private PlayerStateManager playerstate;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerstate = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        if (GameManager.Instance.InputEnabled && GameManager.Instance.MovementEnabled && !GameManager.Instance.playerInConversation)
        {
            HandleInput();
            CalculateMovingDirection();
        }
        if (GameManager.Instance.playerInConversation || GameManager.Instance.inventoryopen)
        {
            xMoveDirection = yMoveDirection= 0;
            currentSpeed = 0;
            playerstate.playerMovement = finalMovingDirection = Vector2.zero;
            _rb.velocity = Vector2.zero;
            
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.InputEnabled && GameManager.Instance.MovementEnabled && !GameManager.Instance.playerInConversation)
        {
            MovePlayer();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(Inputmanager.Instance.Run))
        {
            playerstate.playerRunning = true;
            currentSpeed = runSpeed;
        }
        else
        {
            playerstate.playerRunning = false;
            currentSpeed = walkSpeed;
        }

        if (Inputmanager.Instance != null)
        {
            xMoveDirection = Inputmanager.Instance.InputState == Inputmanager.InputType.MouseKeyboard ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("DPad X");
            yMoveDirection = Inputmanager.Instance.InputState == Inputmanager.InputType.MouseKeyboard ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("DPad Y");
        }
    }

    private void CalculateMovingDirection()
    {
        if (xMoveDirection != 0 || yMoveDirection != 0)
        {
            finalMovingDirection = new Vector2(xMoveDirection, yMoveDirection);
        }

        if (_rb.velocity.x != 0 && xMoveDirection == 0)
        {
            finalMovingDirection = new Vector2(0, finalMovingDirection.y);
        }
        if (_rb.velocity.y != 0 && yMoveDirection == 0)
        {
            finalMovingDirection = new Vector2(finalMovingDirection.x, 0);
        }
        playerstate.playerMovement = finalMovingDirection;
    }

    private void MovePlayer()
    {
        if (currentSpeed > 0)
        {
            _rb.velocity = finalMovingDirection.normalized * currentSpeed;
        }
    }
}
