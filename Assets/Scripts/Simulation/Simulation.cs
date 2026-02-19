using UnityEngine;

public class Simulation
{
    public BumperMan BumperMan1;
    public Ball ball;
    public Vector2 levelDimensions;
    private BumperConfig BumperConfig;
    private BallConfig BallConfig;

    public Simulation(GameObject bumperManPrefab, GameObject ballPrefab, BumperConfig bumperConfig, BallConfig ballConfig)
    {
        this.BumperConfig = bumperConfig;
        this.BallConfig = ballConfig;
        this.SpawnBumperMan(bumperManPrefab, bumperConfig);
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
        this.ball = new Ball(this.BallConfig);
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
        if (ball == null || BumperConfig == null)
        {
            return;
        }

        // Wall collisions
        float halfWidth = levelDimensions.x / 2f;
        float halfHeight = levelDimensions.y / 2f;

        if (ball.position.x - BumperConfig.ballRadius < -halfWidth)
        {
            ball.position.x = -halfWidth + BumperConfig.ballRadius;
            ball.ApplyImpulse(new Vector2(-ball.velocity.x, 0f).normalized, ImpulseType.WALL);
        }
        else if (ball.position.x + BumperConfig.ballRadius > halfWidth)
        {
            ball.position.x = halfWidth - BumperConfig.ballRadius;
            ball.ApplyImpulse(new Vector2(-ball.velocity.x, 0f).normalized, ImpulseType.WALL);
        }

        if (ball.position.y - BumperConfig.ballRadius < -halfHeight)
        {
            ball.position.y = -halfHeight + BumperConfig.ballRadius;
            ball.ApplyImpulse(new Vector2(0f, -ball.velocity.y).normalized, ImpulseType.WALL);
        }
        else if (ball.position.y + BumperConfig.ballRadius > halfHeight)
        {
            ball.position.y = halfHeight - BumperConfig.ballRadius;
            ball.ApplyImpulse(new Vector2(0f, -ball.velocity.y).normalized, ImpulseType.WALL);
        }

        // Player collisions
        if (BumperMan1 != null)
        {
            float combinedRadius = BumperConfig.ballRadius + BumperMan1.radius;
            Vector2 toBall = ball.position - BumperMan1.position;
            float distance = toBall.magnitude;

            if (distance < combinedRadius)
            {
                Vector2 normal = distance > 1e-6f ? toBall / distance : Vector2.up;
                // Move ball out of player
                ball.position = BumperMan1.position + normal * combinedRadius;

                // Simple impulse for now
                Vector2 impulse = normal * BumperConfig.ballBumperImpulseStrength;
                ball.ApplyImpulse(impulse, ImpulseType.BUMPER);
            }
        }
    }
}
