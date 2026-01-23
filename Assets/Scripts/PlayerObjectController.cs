using UnityEngine;

public class PlayerObjectController : MonoBehaviour
{
    public BumperMan simulationBumperMan = null;

    Renderer rend;
    Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rend = GetComponent<Renderer>();
        this.originalColor = this.rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.simulationBumperMan == null)
        {
            this.rend.material.color = Color.red;
            return;
        }

        this.rend.material.color = originalColor;
        this.transform.position = new Vector3(this.simulationBumperMan.position.x, 0f, this.simulationBumperMan.position.y);
    }

    public void setBumperMan(BumperMan bumperMan)
    {
        this.simulationBumperMan = bumperMan;
    }
}
