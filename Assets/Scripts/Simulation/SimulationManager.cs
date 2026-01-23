using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public Simulation simulation;

    public GameObject playerPrefab;

    public float simulationTime = 0f;
    public float lastUpdateTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.simulation = new Simulation(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // this is overkill at the moment, but will let us set a desired tick rate for the sim
        this.lastUpdateTime = this.simulationTime;
        this.simulationTime += Time.deltaTime;

        var playerInput = this.generatePlayerInput();
        this.simulation.update(
            simulationTime: this.simulationTime,
            deltaTime: this.simulationTime - this.lastUpdateTime,
            playerInput: playerInput
        );
    }

    protected PlayerInput generatePlayerInput()
    {
        // on the client:
        // local player gets this from controller
        // remote players get this from the last received input from them
        // on the server:
        // get this from last received.
        return new PlayerInput
        {
        };
    }
}
