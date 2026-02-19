using UnityEngine;


public enum ImpulseType
{
    WALL,
    THROW,
    BUMPER
}

public class Ball
{
    
    public BallConfig config;
    
    public Vector2 position;
    public Vector2 velocity;
    public float timeSinceLastPlayerImpulse;

    public Ball(BallConfig ballConfig)
    {
        this.config = ballConfig;
    }

    public void Update(float deltaTime)
    {
        this.position += this.velocity * deltaTime;
        this.timeSinceLastPlayerImpulse += deltaTime;
    }

    public void ApplyImpulse(Vector2 impulse, ImpulseType impulseType)
    {
        if (this.config == null)
        {
            this.velocity += impulse;
            return;
        }

        if (impulseType == ImpulseType.BUMPER)
        {
            // scale the change in velocity by timeSinceLastPlayerImpulse
            float scale = Mathf.Clamp(timeSinceLastPlayerImpulse, this.config.minBumperImpulse, this.config.maxBumperImpulse);
            this.timeSinceLastPlayerImpulse = 0f;
            this.velocity += impulse * scale;
        }
        else if (impulseType == ImpulseType.THROW)
        {
            this.timeSinceLastPlayerImpulse = 0f;
            this.velocity = (this.velocity + impulse).normalized * 10f; // Could also use a config value for throw speed
        }
        else if (impulseType == ImpulseType.WALL)
        {
            float percentRetention = (this.timeSinceLastPlayerImpulse - this.config.wallRetentionTimeStart) / (this.config.wallRetentionTimeEnd);
            Debug.Log("TOMC__ retention % = " + (percentRetention * 100) + "% (" + this.timeSinceLastPlayerImpulse.ToString("F2")+ ")");
            float velocityRetention = Mathf.Lerp(this.config.maxWallVelocityRetention, this.config.minWallVelocityRetention, percentRetention);
            
            this.velocity += impulse * (2 * velocityRetention * this.velocity.magnitude);
        }

    }
}
