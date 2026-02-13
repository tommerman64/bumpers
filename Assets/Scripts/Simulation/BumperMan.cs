using UnityEngine;

public class BumperMan
{
    public BumperConfig bumperConfig;

    public Vector2 position;
    public Vector2 bumperPosition;
    public float bumperSize;

    public BumperMan(GameObject go, Vector2 position, BumperConfig config)
    {
        this.position = position;
        this.bumperConfig = config;
        this.bumperPosition = Vector2.up;
        this.bumperSize = config != null ? config.maxBumperSize : 120f;
    }

    public void Update(PlayerInput playerInput, float deltaTime)
    {
        float walkSpeed = 3.0f;
        if (playerInput.movementVector != null)
        {
            this.position += (walkSpeed * deltaTime) * playerInput.movementVector.Value;
        }

        if (this.bumperConfig == null) 
        {
            return;
        }

        if (playerInput.bumperInput.HasValue && playerInput.bumperInput.Value.sqrMagnitude > 1e-6f)
        {
            Vector2 targetBumperPosition = playerInput.bumperInput.Value.normalized;
            this.bumperPosition = Vector2.MoveTowards(this.bumperPosition, targetBumperPosition, deltaTime * this.bumperConfig.bumperMoveSpeed);

            float distance = Vector2.Distance(this.bumperPosition, targetBumperPosition);
            // Distance is between 0 and 2.
            // 0 => minBumperSize, 2 => maxBumperSize
            if (bumperSize < Mathf.Epsilon) {
                this.bumperSize = this.bumperConfig.maxBumperSize;
            } else {
                var targetBumperSize = Mathf.Lerp(this.bumperConfig.minBumperSize, this.bumperConfig.maxBumperSize, distance / 2.0f);
                this.bumperSize = Mathf.MoveTowards(this.bumperSize, targetBumperSize, this.bumperConfig.bumperSizeLerpSpeed * deltaTime);
            }
        }
        else
        {
            this.bumperPosition = Vector2.zero;
            this.bumperSize = 0;
        }
    }
}
