using UnityEngine;

public class Spring : MonoBehaviour {
    [SerializeField] private Point referencePointA;
    [SerializeField] private Point referencePointB;
    
    [SerializeField] private float restingLength;
    [SerializeField] private float springConstant;

    public void SetValues(float restingLength, float springConstant, Point referencePointA, Point referencePointB) {
        this.restingLength = restingLength;
        this.springConstant = springConstant;

        this.referencePointA = referencePointA;
        this.referencePointB = referencePointB;
    }

    private void Update() {
        Vector3 posA = referencePointA.transform.position;
        Vector3 posB = referencePointB.transform.position;

        float distance = Vector3.Distance(posA, posB);
        float distFromRest = distance - restingLength;

        float springForce = -springConstant * distFromRest;

        Vector3 normal = (posA - posB).normalized;

        referencePointA.ApplyForce(springForce, normal);
        referencePointB.ApplyForce(springForce, -normal);
        Debug.Log(springForce*-normal);
    }
}