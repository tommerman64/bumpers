using UnityEngine;

public class BumperMan
{
    GameObject gameObject;
    public BumperConfig config;

    public Vector2 position;
    public Vector2 bumperPosition;
    public float bumperSize;

    public BumperMan(GameObject go, Vector2 position, BumperConfig config)
    {
        this.gameObject = go;
        this.position = position;
        this.config = config;
        this.bumperPosition = Vector2.up;
        this.bumperSize = config != null ? config.maxBumperSize : 120f;
    }

    public void update(PlayerInput playerInput, float deltaTime)
    {
        float walkSpeed = this.config != null ? this.config.walkSpeed : 3.0f;
        if (playerInput.movementVector != null)
        {
            this.position += (walkSpeed * deltaTime) * playerInput.movementVector.Value;
        }

        if (this.config == null) return;

        if (playerInput.bumperInput.HasValue && playerInput.bumperInput.Value.sqrMagnitude > 1e-6f)
        {
            Vector2 targetBumperPosition = playerInput.bumperInput.Value.normalized;
            this.bumperPosition = Vector2.MoveTowards(this.bumperPosition, targetBumperPosition, deltaTime * this.config.bumperMoveSpeed);

            float distance = Vector2.Distance(this.bumperPosition, targetBumperPosition);
            // Distance is between 0 and 2.
            // 0 => minBumperSize, 2 => maxBumperSize
            this.bumperSize = Mathf.Lerp(this.config.minBumperSize, this.config.maxBumperSize, distance / 2.0f);
        }
        else
        {
            this.bumperSize = Mathf.MoveTowards(this.bumperSize, this.config.maxBumperSize, deltaTime * this.config.bumperSizeLerpSpeed);
        }
    }
}
