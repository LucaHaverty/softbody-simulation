using UnityEngine;

public class Spring : MonoBehaviour {
    private PointMass referencePointA;
    private PointMass referencePointB;

    private float restingLength;
    private float springConstant;
    private float dampingConstant;

    private LineRenderer _lineRenderer;

    public void SetValues(float springConstant, float dampingConstant, PointMass referencePointA,
        PointMass referencePointB) {
        
        this.restingLength = Vector3.Distance(referencePointA.transform.position, referencePointB.transform.position);
        this.springConstant = springConstant;
        this.dampingConstant = dampingConstant;

        this.referencePointA = referencePointA;
        this.referencePointB = referencePointB;

        _lineRenderer = GetComponent<LineRenderer>();
        UpdateLineRenderer();
    }

    public void ApplyForceToPoints() {

        // Spring force
        Vector3 posA = referencePointA.transform.position;
        Vector3 posB = referencePointB.transform.position;

        float distance = Vector3.Distance(posA, posB);
        float distFromRest = distance - restingLength;

        float springForce = -springConstant * distFromRest;

        // Damping force (scale based of velocity of points towards each other)
        Vector3 normal = (posA - posB).normalized;
        float dampingForce = Vector3.Dot(normal, referencePointB.Velocity - referencePointA.Velocity) * dampingConstant;

        // Apply force to points
        float netForce = springForce + dampingForce;

        referencePointA.ApplyForce(netForce, normal);
        referencePointB.ApplyForce(netForce, -normal);
    }

    public void UpdateLineRenderer() {
        _lineRenderer.SetPosition(0, referencePointA.transform.position);
        _lineRenderer.SetPosition(1, referencePointB.transform.position);
    }
}