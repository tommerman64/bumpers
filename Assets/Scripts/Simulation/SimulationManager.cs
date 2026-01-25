using UnityEngine;
using UnityEngine.InputSystem;

public class SimulationManager : MonoBehaviour
{
    public Simulation simulation;

    public GameObject playerPrefab;
    public PlayerInputController playerInputController;

    public float simulationTime = 0f;
    public float lastUpdateTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.simulation = new Simulation(playerPrefab);
        this.playerInputController?.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // this is overkill at the moment, but will let us set a desired tick rate for the sim
        this.lastUpdateTime = this.simulationTime;
        this.simulationTime += Time.deltaTime;

        var playerInput = this.generatePlayerInput(this.simulationTime);
        this.simulation.update(
            simulationTime: this.simulationTime,
            deltaTime: this.simulationTime - this.lastUpdateTime,
            playerInput: playerInput
        );
    }

    protected PlayerInput generatePlayerInput(float simulationTime)
    {
        if (this.playerInputController!= null)
        {
            return this.playerInputController.generatePlayerInput(simulationTime);
        }

        return new PlayerInput{};
    }

    void OnDisable()
    {
        this.playerInputController?.Disable();
    }
}
