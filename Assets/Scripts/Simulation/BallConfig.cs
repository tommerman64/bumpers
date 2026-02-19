using UnityEngine;

[CreateAssetMenu(fileName = "BallConfig", menuName = "Simulation/BallConfig")]
public class BallConfig : ScriptableObject
{
    public float minBumperImpulse = 0.2f;
    public float maxBumperImpulse = 2f;


    public float wallRetentionTimeStart = 0.2f;
    public float wallRetentionTimeEnd = 3f;
    public float maxWallVelocityRetention = 1.0f;
    public float minWallVelocityRetention = 0.1f;
}
