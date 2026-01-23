using UnityEngine;
using UnityEngine.InputSystem;

public class SimulationManager : MonoBehaviour
{
    public Simulation simulation;

    public GameObject playerPrefab;
    public InputConfig inputConfig;

    public float simulationTime = 0f;
    public float lastUpdateTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.simulation = new Simulation(playerPrefab);
        this.inputConfig.dashAction.action.Enable();
        this.inputConfig.bumperAction.action.Enable();
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
        return new PlayerInput
        {
            dashInput = this.inputConfig.dashAction.action.ReadValue<Vector2>(),
            bumperInput = this.inputConfig.bumperAction.action.ReadValue<Vector2>()
        };
    }

    void OnDisable()
    {
        if (inputConfig != null)
        {
            inputConfig.dashAction.action.Disable();
            inputConfig.bumperAction.action.Disable();
        }
    }
}
