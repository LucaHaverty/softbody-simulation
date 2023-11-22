using System.Collections.Generic;
using UnityEngine;

public class SimulationRunner : MonoBehaviour {
    public static SimulationRunner Instance;

    private readonly List<SimObject> _simObjects = new();

    private void Awake() {
        Instance = this;
        
        _simObjects.Add(new Cube(springConstant: 10, width: 5, length: 5, height: 5));
    }

    private void Update() {
        _simObjects.ForEach(s => {
            s.Update();
        });
    }

    public static PointMass InstantiatePoint(Vector3 position, Transform parent, float mass) {
        var point = Instantiate(SimulationConfig.PointPrefab, position, Quaternion.identity, parent)
            .GetComponent<PointMass>();

        point.SetValues(mass);
        return point;
    }

    public static Spring InstantiateSpring(Transform parent, float springConstant, float dampingConstant,
        PointMass refA, PointMass refB) {

        var spring = Instantiate(SimulationConfig.SpringPrefab,
            (refA.transform.position + refB.transform.position) / 2,
            Quaternion.identity, parent).GetComponent<Spring>();

        spring.SetValues(springConstant, dampingConstant, refA, refB);
        return spring;
    }
}
