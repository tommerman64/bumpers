using UnityEngine;

public class Simulation
{
    public BumperMan BumperMan1;
    public Ball ball;
    public Vector2 levelDimensions;
    public BumperConfig config;

    public Simulation(GameObject bumperManPrefab, GameObject ballPrefab, BumperConfig config)
    {
        this.config = config;
        this.SpawnBumperMan(bumperManPrefab, config);
        this.SpawnBall(ballPrefab);
    }

    public static Vector3 SimPositionSwizzle(Vector2 simPosition)
    {
        return new Vector3(simPosition.x, 0f, simPosition.y);
    }

    private void SpawnBumperMan(GameObject prefab, BumperConfig config)
    {
        var bumperManGameObject = GameObject.Instantiate(prefab);
        this.BumperMan1 = new BumperMan(bumperManGameObject, position: Vector2.zero, config: config);

        foreach (var view in bumperManGameObject.GetComponents<IBumperManEntity>())
        {
            view.SetBumperMan(this.BumperMan1);
        }
    }

    private void SpawnBall(GameObject prefab)
    {
        var ballGameObject = GameObject.Instantiate(prefab);
        this.ball = new Ball();
        ballGameObject.GetComponent<BallObjectController>().SetBall(this.ball);
    }

    public void Update(float simulationTime, float deltaTime, PlayerInput playerInput)
    {
        this.BumperMan1.Update(playerInput, deltaTime);
        this.ball?.Update(deltaTime);
        this.ResolveBallCollisions();
    }

    public void ResolveBallCollisions()
    {
        if (ball == null || config == null)
        {
            return;
        }

        // Wall collisions
        float halfWidth = levelDimensions.x / 2f;
        float halfHeight = levelDimensions.y / 2f;

        if (ball.position.x - config.ballRadius < -halfWidth)
        {
            ball.position.x = -halfWidth + config.ballRadius;
            ball.ApplyImpulse(new Vector2(-2f * ball.velocity.x, 0f), ImpulseType.WALL, config);
        }
        else if (ball.position.x + config.ballRadius > halfWidth)
        {
            ball.position.x = halfWidth - config.ballRadius;
            ball.ApplyImpulse(new Vector2(-2f * ball.velocity.x, 0f), ImpulseType.WALL, config);
        }

        if (ball.position.y - config.ballRadius < -halfHeight)
        {
            ball.position.y = -halfHeight + config.ballRadius;
            ball.ApplyImpulse(new Vector2(0f, -2f * ball.velocity.y), ImpulseType.WALL, config);
        }
        else if (ball.position.y + config.ballRadius > halfHeight)
        {
            ball.position.y = halfHeight - config.ballRadius;
            ball.ApplyImpulse(new Vector2(0f, -2f * ball.velocity.y), ImpulseType.WALL, config);
        }

        // Player collisions
        if (BumperMan1 != null)
        {
            float combinedRadius = config.ballRadius + BumperMan1.radius;
            Vector2 toBall = ball.position - BumperMan1.position;
            float distance = toBall.magnitude;

            if (distance < combinedRadius)
            {
                Vector2 normal = distance > 1e-6f ? toBall / distance : Vector2.up;
                // Move ball out of player
                ball.position = BumperMan1.position + normal * combinedRadius;

                // Simple impulse for now
                Vector2 impulse = normal * config.ballBumperImpulseStrength;
                ball.ApplyImpulse(impulse, ImpulseType.BUMPER, config);
            }
        }
    }
}
