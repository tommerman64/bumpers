using UnityEngine;

public class Simulation
{
    public BumperMan bumperMan1;

    public Simulation(GameObject bumperManPrefab)
    {
        this.spawnBumperMan(bumperManPrefab);
    }

    public void spawnBumperMan(GameObject prefab)
    {
        var bumperManGameObject = GameObject.Instantiate(prefab);
        this.bumperMan1 = new BumperMan(bumperManGameObject, position: Vector2.zero);
        bumperManGameObject.GetComponent<PlayerObjectController>().setBumperMan(this.bumperMan1);
    }

    public void update(float simulationTime, float deltaTime, PlayerInput playerInput)
    {
        this.bumperMan1.update(playerInput, deltaTime);
    }
}
