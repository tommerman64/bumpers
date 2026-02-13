using UnityEngine;

public class Simulation
{
    public BumperMan BumperMan1;
    public Ball ball;

    public Simulation(GameObject bumperManPrefab, GameObject ballPrefab, BumperConfig config)
    {
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
    }

    public void ResolveBallCollisions()
    {
        if (ball == null)
        {
            return;
        }
        
        
    }
}
