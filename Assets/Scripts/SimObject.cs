using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SimObject {
    protected readonly GameObject _rootGameObject;

    protected List<PointMass> _points;
    protected readonly List<Spring> _springs = new();

    private Vector3 _estimatedPosition = Vector3.zero;
    private Vector3 _estimatedVelocity = Vector3.zero;
    private Vector3 _estimatedAcceleration = Vector3.zero;

    public Vector3 EstimatedPosition => _estimatedPosition;
    public Vector3 EstimatedVelocity => _estimatedVelocity;
    public Vector3 EstimatedAcceleration => _estimatedAcceleration;

    protected SimObject(string name) {
        _rootGameObject = new(name) {
            transform = {
                position = Vector3.up * SimConfig.StartHeight
            }
        };
        _rootGameObject.transform.parent = SimConfig.Sim.transform;
    }

    public void Update() {
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

        var prevPos = _estimatedPosition;

        Vector3 sum = Vector3.zero;
        _points.ForEach(p => sum += p.transform.position);
        
        _estimatedPosition = sum / _points.Count;

        if (prevPos.Equals(Vector3.zero))
            return;

        var prevVel = _estimatedVelocity;
        
        _estimatedVelocity = (_estimatedPosition - prevPos) / Time.fixedDeltaTime;

        if (prevVel.Equals(Vector3.zero))
            return;
        
        _estimatedAcceleration = (_estimatedVelocity - prevVel) / Time.fixedDeltaTime;
    }

    public void DestroyRootObject() {
        Object.Destroy(_rootGameObject); 
    }
}

public class Cube : SimObject {
    public Cube(int length, int width, int height, 
        float pointSpacing)
        : base($"Cube ({length}, {width}, {height})") {

        // Center offset
        _rootGameObject.transform.Translate(-new Vector3(length*pointSpacing/2, width*pointSpacing/2, height*pointSpacing/2));

        Dictionary<Vector3Int, PointMass> pointsDict = new();

        for (int h = 0; h < height; h++) {
            for (int w = 0; w < width; w++) {
                for (int l = 0; l < length; l++) {
                    pointsDict.Add(new Vector3Int(l,w,h), SimRunner.InstantiatePoint(
                        Vector3.up * (pointSpacing * h) + Vector3.right * (pointSpacing * w)
                                                        + Vector3.forward * (pointSpacing * l),
                        _rootGameObject.transform));
                }
            }
        }

        void AttemptAddConnection(PointMass pointA, Vector3Int posB) {
            if (!pointsDict.TryGetValue(posB, out var pointB)) 
                return;
            
            if (pointA == pointB)
                return;
                
            _springs.Add(SimRunner.InstantiateSpring(
                _rootGameObject.transform, pointA, pointB));
        }
        
        foreach (var kvp in pointsDict) {
            for (int h = 0; h <= 1; h++) {
                for (int w = 0; w <= 1; w++) {
                    for (int l = 0; l <= 1; l++) {
                        AttemptAddConnection(kvp.Value, kvp.Key + new Vector3Int(l, w, h));
                    }
                }
            }
        }

        _points = pointsDict.Values.ToList();
    }
    
    /*public class Rope : SimObject {
    public Rope(int numPoints = 5, float pointSpacing = 0.5f, float pointMass = 1, float springConstant = 8,
        float dampingConstant = 0.1f)
        : base($"Rope ({numPoints})") {

        for (int p = 0; p < numPoints; p++) {
            _points.Add(SimulationRunner.InstantiatePoint(
                Vector3.up * (pointSpacing * p), _rootGameObject.transform, pointMass));
        }

        for (int s = 0; s < numPoints - 1; s++) {
            _springs.Add(SimulationRunner.InstantiateSpring(
                _rootGameObject.transform, springConstant, dampingConstant, _points[s], _points[s + 1]));
        }
    }
}

public class Plane : SimObject {
    public Plane(int width = 5, int height = 5, float pointSpacing = 0.5f, float pointMass = 1,
        float springConstant = 5, float dampingConstant = 0.1f)
        : base($"Plane ({width}, {height})") {

        for (int p = 0; p < width; p++) {
            for (int p1 = 0; p1 < height; p1++) {
                _points.Add(SimulationRunner.InstantiatePoint(
                    Vector3.up * (pointSpacing * p) + Vector3.right * (pointSpacing * p1),
                    _rootGameObject.transform, pointMass));
            }
        }

        for (int s = 0; s < width - 1; s++) {
            for (int s1 = 0; s1 < width - 1; s1++) {
                int startIndex = s + (width * s1);

                _springs.Add(SimulationRunner.InstantiateSpring(
                    _rootGameObject.transform, springConstant, dampingConstant, _points[startIndex],
                    _points[startIndex + 1]));

                _springs.Add(SimulationRunner.InstantiateSpring(
                    _rootGameObject.transform, springConstant, dampingConstant, _points[startIndex],
                    _points[startIndex + width]));

                _springs.Add(SimulationRunner.InstantiateSpring(
                    _rootGameObject.transform, springConstant, dampingConstant, _points[startIndex],
                    _points[startIndex + width + 1]));
            }
        }
    }
}*/
}