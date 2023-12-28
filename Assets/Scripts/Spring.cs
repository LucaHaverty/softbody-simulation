using UnityEngine;

public class Spring : MonoBehaviour {
    public PointMass referencePointA;
    public PointMass referencePointB;

    private float restingLength;

    private LineRenderer _lineRenderer;

    public void SetRefs(PointMass referencePointA,
        PointMass referencePointB) {
        
        restingLength = Vector3.Distance(referencePointA.transform.position, referencePointB.transform.position);

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

        float springForce = -SimConfig.SpringConstant * distFromRest;

        // Damping force (scale based of velocity of points towards each other)
        Vector3 normal = (posA - posB).normalized;
        float dampingForce = Vector3.Dot(normal, referencePointB.Velocity - referencePointA.Velocity) 
                             * SimConfig.DampingConstant;

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