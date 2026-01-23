using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Simulation/InputConfig")]
public class InputConfig : ScriptableObject
{
    public InputActionReference dashAction;
    public InputActionReference bumperAction;
}
