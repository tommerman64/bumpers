using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Simulation/InputConfig")]
public class InputConfig : ScriptableObject
{
    public InputActionReference moveVectorAction;

    public InputActionReference up;
    public InputActionReference down;
    public InputActionReference left;
    public InputActionReference right;


    public InputActionReference bumperVectorAction;
}
