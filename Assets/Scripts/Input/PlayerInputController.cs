using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInputController
{
    [SerializeField]
    InputConfig inputConfig;

    private float lastDashTime = -1f;

    public void Enable()
    {
        if (this.inputConfig == null) return;

        this.inputConfig.moveVectorAction?.action.Enable();
        this.inputConfig.up?.action.Enable();
        this.inputConfig.down?.action.Enable();
        this.inputConfig.left?.action.Enable();
        this.inputConfig.right?.action.Enable();
        this.inputConfig.bumperVectorAction?.action.Enable();
        this.inputConfig.dashAction?.action.Enable();
    }

    public PlayerInput generatePlayerInput(float simulationTime)
    {
        if (this.inputConfig == null)
        {
            return new PlayerInput { };
        }

        if (this.inputConfig.dashAction != null && this.inputConfig.dashAction.action.WasPerformedThisFrame())
        {
            this.lastDashTime = simulationTime;
        }

        Vector2 moveVector = Vector2.zero;
        Vector2 moveVectorFromVectorAction = this.inputConfig.moveVectorAction == null ? Vector2.zero : this.inputConfig.moveVectorAction.action.ReadValue<Vector2>();
        if (moveVectorFromVectorAction.sqrMagnitude > Mathf.Epsilon)
        {
            moveVector = this.inputConfig.moveVectorAction.action.ReadValue<Vector2>();
        }
        else
        {
            if (this.inputConfig.up.action.IsPressed()) moveVector.y += 1;
            if (this.inputConfig.down.action.IsPressed()) moveVector.y -= 1;
            if (this.inputConfig.left.action.IsPressed()) moveVector.x -= 1;
            if (this.inputConfig.right.action.IsPressed()) moveVector.x += 1;
            moveVector.Normalize();

        }

        Vector2 bumperVector = Vector2.zero;
        if (this.inputConfig.bumperVectorAction != null)
        {
            bumperVector = this.inputConfig.bumperVectorAction.action.ReadValue<Vector2>();
        }

        return new PlayerInput
        {
            movementVector = moveVector,
            bumperInput = bumperVector,
            lastDashTime = this.lastDashTime,
        };
    }

    public void Disable()
    {
        if (this.inputConfig == null) return;

        this.inputConfig.moveVectorAction?.action.Disable();
        this.inputConfig.up?.action.Disable();
        this.inputConfig.down?.action.Disable();
        this.inputConfig.left?.action.Disable();
        this.inputConfig.right?.action.Disable();
        this.inputConfig.bumperVectorAction?.action.Disable();
        this.inputConfig.dashAction?.action.Disable();
    }
}
