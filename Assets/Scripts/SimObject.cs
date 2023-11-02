using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimObject {
    protected GameObject _rootGameObject;

    protected List<PointMass> _points;
    protected List<Spring> _springs;

    protected SimObject(string name) {
        _rootGameObject = new GameObject(name);
    }

    public void UpdatePoints() {
        throw new NotImplementedException();
    }

    public void UpdateSprings() {
        throw new NotImplementedException();
    }
}

public class Rope : SimObject {
    public Rope(int numPoints, float pointSpacing, float pointMass, float springConstant) 
        : base($"Rope ({numPoints})") {
        
        for (int p = 0; p < numPoints; p++) {
            _points.Add(SimulationRunner.InstantiatePoint(
                Vector3.up * (pointSpacing * p), _rootGameObject.transform, pointMass));
        }

        for (int s = 0; s < numPoints - 1; s++) {
            _springs.Add(SimulationRunner.InstantiateSpring(
                _rootGameObject.transform, springConstant, _points[s], _points[s-1]));
        }
    }
}

public class Plane : SimObject {
    public Plane(int length, int height, float pointSpacing, float pointMass, float springConstant) 
        : base($"Plane ({length}, {height})") {

        throw new NotImplementedException();
    }
}

public class Cube : SimObject {
    public Cube(int length, int width, int height, float pointSpacing, float pointMass, float springConstant) 
        : base($"Plane ({length}, {width}, {height})") {

        throw new NotImplementedException();
    }
}