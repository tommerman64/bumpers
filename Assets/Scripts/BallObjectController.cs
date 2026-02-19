using UnityEngine;

public class BallObjectController : MonoBehaviour
{
    private Ball simulationBall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initialize effects
    }

    // Update is called once per frame
    void Update()
    {
        if (simulationBall == null)
        {
            return;
        }

        this.transform.position = Simulation.SimPositionSwizzle(simulationBall.position);
    }

    public void SetBall(Ball ball)
    {
        this.simulationBall = ball;
    }
}
