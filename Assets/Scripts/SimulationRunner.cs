using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationRunner : MonoBehaviour {
    public static SimulationRunner Instance;

    private List<SimObject> _simObjects = new();

    private void Awake() {
        Instance = this;
        
        _simObjects.Add(new Cube());
    }

    public static PointMass InstantiatePoint(Vector3 position, Transform parent, float mass) {
        var point = Instantiate(SimulationConfig.PointPrefab, position, Quaternion.identity, parent)
            .GetComponent<PointMass>();

        point.SetValues(mass);
        return point;
    }

    public static Spring InstantiateSpring(Transform parent, float springConstant,
        float restingLength, PointMass refA, PointMass refB) {

        var spring = Instantiate(SimulationConfig.SpringPrefab,
                (refA.transform.position + refB.transform.position) / 2, 
                Quaternion.identity, parent).GetComponent<Spring>();

        spring.SetValues(springConstant, restingLength, refA, refB);
        return spring;
    }
    
    public static Spring InstantiateSpring(Transform parent, float springConstant, PointMass refA, PointMass refB) {
        return InstantiateSpring(parent, springConstant, 
            Vector3.Distance(refA.transform.position, refB.transform.position), refA, refB);
    }
}
