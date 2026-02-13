using UnityEngine;


public enum ImpulseType
{
    WALL,
    THROW,
    BUMPER
}

public class Ball
{
    public Vector2 position;
    public Vector2 velocity;
    public float timeSinceLastPlayerImpulse;

    public void Update(float deltaTime)
    {
        this.timeSinceLastPlayerImpulse += deltaTime;
    }

    public void ApplyImpulse(Vector2 impulse, ImpulseType impulseType)
    {
        if (impulseType is ImpulseType.THROW or ImpulseType.BUMPER)
        {
            this.timeSinceLastPlayerImpulse = 0f;
        }
        
        // if throw,normalize velocity
        // if bumper scale velocity based on timeSinceLastPlayerImpulse
        // if wall, decay velocity based on timeSinceLastPlayerImpulse
    }
}
