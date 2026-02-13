using UnityEngine;

public class BumperVisuals : MonoBehaviour, IBumperManEntity
{
    private BumperMan simulationBumperMan = null;

    public Transform bumperTransform;
    public LineRenderer bumperLineRenderer;
    public float arcRadius = 1.0f;
    public int arcSegments = 24;

    void Start()
    {
        if (this.bumperLineRenderer != null)
        {
            this.bumperLineRenderer.useWorldSpace = false;
        }
    }

    void Update()
    {
        if (this.simulationBumperMan == null) return;

        if (this.bumperTransform != null)
        {
            this.bumperTransform.localPosition = new Vector3(this.simulationBumperMan.bumperPosition.x, 0, this.simulationBumperMan.bumperPosition.y);
        }

        if (this.bumperLineRenderer != null)
        {
            this.DrawArc(this.arcRadius, this.simulationBumperMan.bumperPosition, this.simulationBumperMan.bumperSize);
        }
    }

    void DrawArc(float radius, Vector2 direction, float angleDegrees)
    {
        if (this.bumperLineRenderer == null) return;

        float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = centerAngle - angleDegrees / 2f;
        float endAngle = centerAngle + angleDegrees / 2f;

        this.bumperLineRenderer.positionCount = arcSegments + 1;
        for (int i = 0; i <= arcSegments; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, (float)i / arcSegments);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            this.bumperLineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }

    public void SetBumperMan(BumperMan bumperMan)
    {
        this.simulationBumperMan = bumperMan;
    }
}
