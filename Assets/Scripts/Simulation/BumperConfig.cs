using UnityEngine;

[CreateAssetMenu(fileName = "BumperConfig", menuName = "Simulation/BumperConfig")]
public class BumperConfig : ScriptableObject
{
    public float walkSpeed = 3.0f;
    public float bumperMoveSpeed = 5.0f;
    public float minBumperSize = 15.0f;
    public float maxBumperSize = 120.0f;
    public float bumperSizeLerpSpeed = 5.0f;
}
