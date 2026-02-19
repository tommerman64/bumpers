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
        this.position += this.velocity * deltaTime;
        this.timeSinceLastPlayerImpulse += deltaTime;
    }

    public void ApplyImpulse(Vector2 impulse, ImpulseType impulseType, BumperConfig config)
    {
        float scale = 1.0f;
        if (config == null)
        {
            this.velocity += impulse;
            return;
        }

        if (impulseType == ImpulseType.BUMPER)
        {
            // scale the change in velocity by timeSinceLastPlayerImpulse
            scale = Mathf.Clamp(timeSinceLastPlayerImpulse, 0.1f, 2.0f);
            this.timeSinceLastPlayerImpulse = 0f;
        }
        else if (impulseType == ImpulseType.THROW)
        {
            this.timeSinceLastPlayerImpulse = 0f;
            this.velocity = (this.velocity + impulse).normalized * 10f; // Could also use a config value for throw speed
            return;
        }
        else if (impulseType == ImpulseType.WALL)
        {
            // decay velocity based on timeSinceLastPlayerImpulse using ballDecayRate and ballWallRestitution
            float decay = Mathf.Max(0f, 1.0f - timeSinceLastPlayerImpulse * config.ballDecayRate);
            scale = config.ballWallRestitution * decay;
        }

        this.velocity += impulse * scale;
    }
}
