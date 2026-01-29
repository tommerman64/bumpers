using UnityEngine;

public class Simulation
{
    public BumperMan bumperMan1;

    public Simulation(GameObject bumperManPrefab, BumperConfig config)
    {
        this.spawnBumperMan(bumperManPrefab, config);
    }

    public void spawnBumperMan(GameObject prefab, BumperConfig config)
    {
        var bumperManGameObject = GameObject.Instantiate(prefab);
        this.bumperMan1 = new BumperMan(bumperManGameObject, position: Vector2.zero, config: config);
        bumperManGameObject.GetComponent<PlayerObjectController>().setBumperMan(this.bumperMan1);
    }

    public void update(float simulationTime, float deltaTime, PlayerInput playerInput)
    {
        this.bumperMan1.update(playerInput, deltaTime);
    }
}
