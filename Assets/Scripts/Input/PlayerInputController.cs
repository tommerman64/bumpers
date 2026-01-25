using System;
using UnityEngine;

[Serializable]
public class PlayerInputController
{
    [SerializeField]
    InputConfig inputConfig;

    

    public void Enable()
    {
        if (this.inputConfig != null)
        {
            this.inputConfig.bumperVectorAction.action.Enable();
        }
    }

    public PlayerInput generatePlayerInput()
    {
        

        return new PlayerInput{};
    }

    public void Disable()
    {
        if (this.inputConfig != null)
        {
            this.inputConfig.bumperVectorAction.action.Disable();
        }
    }
}
