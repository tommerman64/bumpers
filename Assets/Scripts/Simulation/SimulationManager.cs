using UnityEngine;
using UnityEngine.InputSystem;

public class SimulationManager : MonoBehaviour
{
    public Simulation simulation;

    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public BumperConfig bumperConfig;
    public PlayerInputController playerInputController;
    public BallConfig BallConfig;
    
    public float simulationTime = 0f;
    public float lastUpdateTime = 0f;

    public Vector2 levelDimensions = new Vector2(20f, 20f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.simulation = new Simulation(bumperManPrefab: playerPrefab, ballPrefab: ballPrefab, bumperConfig: bumperConfig, ballConfig: BallConfig);
        this.simulation.levelDimensions = this.levelDimensions;
        
        this.playerInputController?.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // this is overkill at the moment, but will let us set a desired tick rate for the sim
        this.lastUpdateTime = this.simulationTime;
        this.simulationTime += Time.deltaTime;

        var playerInput = this.generatePlayerInput(this.simulationTime);
        this.simulation.Update(
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
