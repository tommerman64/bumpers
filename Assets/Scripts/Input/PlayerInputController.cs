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
        this.inputConfig.bumperUp?.action.Enable();
        this.inputConfig.bumperDown?.action.Enable();
        this.inputConfig.bumperLeft?.action.Enable();
        this.inputConfig.bumperRight?.action.Enable();

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
            moveVector = moveVectorFromVectorAction;
        }
        else
        {
            if (this.inputConfig.up != null && this.inputConfig.up.action.IsPressed()) moveVector.y += 1;
            if (this.inputConfig.down != null && this.inputConfig.down.action.IsPressed()) moveVector.y -= 1;
            if (this.inputConfig.left != null && this.inputConfig.left.action.IsPressed()) moveVector.x -= 1;
            if (this.inputConfig.right != null && this.inputConfig.right.action.IsPressed()) moveVector.x += 1;
            moveVector.Normalize();
        }

        Vector2 bumperVector = Vector2.zero;
        Vector2 bumperVectorFromVectorAction = this.inputConfig.bumperVectorAction == null ? Vector2.zero : this.inputConfig.bumperVectorAction.action.ReadValue<Vector2>();
        if (bumperVectorFromVectorAction.sqrMagnitude > Mathf.Epsilon)
        {
            bumperVector = bumperVectorFromVectorAction;
        }
        else
        {
            if (this.inputConfig.bumperUp != null && this.inputConfig.bumperUp.action.IsPressed()) bumperVector.y += 1;
            if (this.inputConfig.bumperDown != null && this.inputConfig.bumperDown.action.IsPressed()) bumperVector.y -= 1;
            if (this.inputConfig.bumperLeft != null && this.inputConfig.bumperLeft.action.IsPressed()) bumperVector.x -= 1;
            if (this.inputConfig.bumperRight != null && this.inputConfig.bumperRight.action.IsPressed()) bumperVector.x += 1;
            // We don't necessarily want to normalize if it's zero, but BumperMan handles normalization of non-zero input.
            // However, for consistency with moveVector, we can normalize here if non-zero.
            if (bumperVector.sqrMagnitude > Mathf.Epsilon)
            {
                bumperVector.Normalize();
            }
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
        this.inputConfig.bumperUp?.action.Disable();
        this.inputConfig.bumperDown?.action.Disable();
        this.inputConfig.bumperLeft?.action.Disable();
        this.inputConfig.bumperRight?.action.Disable();

        this.inputConfig.dashAction?.action.Disable();
    }
}
