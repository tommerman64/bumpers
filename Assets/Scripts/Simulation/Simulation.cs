using UnityEngine;

public class Simulation
{
    public BumperMan BumperMan1;

    public Simulation(GameObject bumperManPrefab, BumperConfig config)
    {
        this.SpawnBumperMan(bumperManPrefab, config);
    }

    public void SpawnBumperMan(GameObject prefab, BumperConfig config)
    {
        var bumperManGameObject = GameObject.Instantiate(prefab);
        this.BumperMan1 = new BumperMan(bumperManGameObject, position: Vector2.zero, config: config);

        foreach (var view in bumperManGameObject.GetComponents<IBumperManEntity>())
        {
            view.SetBumperMan(this.BumperMan1);
        }
    }

    public void Update(float simulationTime, float deltaTime, PlayerInput playerInput)
    {
        this.BumperMan1.Update(playerInput, deltaTime);
    }
}
