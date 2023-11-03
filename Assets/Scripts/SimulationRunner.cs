using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationRunner : MonoBehaviour {
    public static SimulationRunner Instance;

    private List<SimObject> _simObjects = new();

    private List<PointMass> _points = new();
    private List<Spring> _springs = new();

    private void Awake() {
        Instance = this;
        
        _simObjects.Add(new Cube());
    }

    private void Update() {
        _springs.ForEach(s => {
            s.ApplyForceToPoints();
        });
        
        _points.ForEach(p => {
            p.ApplyGravity();
            p.UpdatePosition();
        });
        
        _springs.ForEach(s => {
            s.UpdateLineRenderer();
        });
    }

    public static PointMass InstantiatePoint(Vector3 position, Transform parent, float mass) {
        var point = Instantiate(SimulationConfig.PointPrefab, position, Quaternion.identity, parent)
            .GetComponent<PointMass>();

        point.SetValues(mass);
        Instance._points.Add(point);

        return point;
    }

    public static Spring InstantiateSpring(Transform parent, float springConstant,
        float restingLength, PointMass refA, PointMass refB) {

        var spring = Instantiate(SimulationConfig.SpringPrefab,
                (refA.transform.position + refB.transform.position) / 2, 
                Quaternion.identity, parent).GetComponent<Spring>();

        spring.SetValues(springConstant, restingLength, refA, refB);
        
        Instance._springs.Add(spring);
        return spring;
    }
    
    public static Spring InstantiateSpring(Transform parent, float springConstant, PointMass refA, PointMass refB) {
        return InstantiateSpring(parent, springConstant, 
            Vector3.Distance(refA.transform.position, refB.transform.position), refA, refB);
    }
}
