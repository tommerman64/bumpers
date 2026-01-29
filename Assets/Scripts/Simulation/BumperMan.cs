using UnityEngine;

public class BumperMan
{
    private static float WALK_SPEED = 3.0f;
    GameObject gameObject;

    public Vector2 position;
    public Vector2 bumperPosition;
    public float bumperSize;

    public BumperMan(GameObject go, Vector3 position)
    {
        this.gameObject = go;
        this.position = position;
        this.bumperPosition = Vector2.zero;
        this.bumperSize = 0f;
    }

    public void update(PlayerInput playerInput, float deltaTime)
    {
        if (playerInput.movementVector != null)
        {
            this.position += (WALK_SPEED * deltaTime) * playerInput.movementVector.Value;
        }
    }
    
}
