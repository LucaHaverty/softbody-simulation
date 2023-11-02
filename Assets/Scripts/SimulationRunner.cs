using System.Collections.Generic;
using UnityEngine;

public class SimulationRunner : MonoBehaviour {
    public static SimulationRunner Instance;

    public GameObject PointPrefab;
    public GameObject SpringPrefab;

    private List<PointMass> _points;
    private List<Spring> _springs;

    private void Awake() {
        Instance = this;
    }

    public static PointMass InstantiatePoint(Vector3 position, Transform parent, float mass) {
        var point = Instantiate(Instance.PointPrefab, position, Quaternion.identity, parent)
            .GetComponent<PointMass>();

        point.SetValues(mass);
        return point;
    }

    public static Spring InstantiateSpring(Transform parent, float springConstant,
        float restingLength, PointMass refA, PointMass refB) {

        var spring = Instantiate(Instance.SpringPrefab,
                (refA.transform.position + refB.transform.position) / 2, 
                Quaternion.identity, parent)
            .GetComponent<Spring>();

        spring.SetValues(springConstant, restingLength, refA, refB);
        return spring;
    }
    
    public static Spring InstantiateSpring(Transform parent, float springConstant, PointMass refA, PointMass refB) {
        return InstantiateSpring(parent, springConstant, 
            Vector3.Distance(refA.transform.position, refB.transform.position), refA, refB);
    }
}
