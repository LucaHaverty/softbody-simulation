using UnityEngine;

public class Spring : MonoBehaviour {
    private PointMass referencePointA;
    private PointMass referencePointB;
    
    private float restingLength;
    private float springConstant;

    private LineRenderer _lineRenderer;

    public void SetValues(float restingLength, float springConstant, PointMass referencePointA, PointMass referencePointB) {
        this.restingLength = restingLength;
        this.springConstant = springConstant;

        this.referencePointA = referencePointA;
        this.referencePointB = referencePointB;

        _lineRenderer = GetComponent<LineRenderer>();
        UpdateLineRenderer();
    }

    private void Update() {
        ApplyForceToPoints();
        UpdateLineRenderer();
    }

    public void ApplyForceToPoints() {
        Vector3 posA = referencePointA.transform.position;
        Vector3 posB = referencePointB.transform.position;

        float distance = Vector3.Distance(posA, posB);
        float distFromRest = distance - restingLength;

        float springForce = -springConstant * distFromRest;

        Vector3 normal = (posA - posB).normalized;

        referencePointA.ApplyForce(springForce, normal);
        referencePointB.ApplyForce(springForce, -normal);
    }

    public void UpdateLineRenderer() {
        _lineRenderer.SetPosition(0, referencePointA.transform.position);
        _lineRenderer.SetPosition(1, referencePointB.transform.position);
    }
}