using UnityEngine;

public class PlayerObjectController : MonoBehaviour, IBumperManEntity
{
    public BumperMan simulationBumperMan = null;

    Renderer rend;
    Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rend = GetComponent<Renderer>();
        if (this.rend != null)
        {
            this.originalColor = this.rend.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.simulationBumperMan == null)
        {
            if (this.rend != null) this.rend.material.color = Color.red;
            return;
        }

        if (this.rend != null) this.rend.material.color = originalColor;
        this.transform.position = new Vector3(this.simulationBumperMan.position.x, 0f, this.simulationBumperMan.position.y);
    }

    public void SetBumperMan(BumperMan bumperMan)
    {
        this.simulationBumperMan = bumperMan;
    }
}
