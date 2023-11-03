using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimObject {
    protected readonly GameObject _rootGameObject;

    protected readonly List<PointMass> _points = new();
    protected readonly List<Spring> _springs = new();

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
    public Rope(int numPoints = 5, float pointSpacing = 0.5f, float pointMass = 1, float springConstant = 8) 
        : base($"Rope ({numPoints})") {
        
        for (int p = 0; p < numPoints; p++) {
            _points.Add(SimulationRunner.InstantiatePoint(
                Vector3.up * (pointSpacing * p), _rootGameObject.transform, pointMass));
        }

        for (int s = 0; s < numPoints - 1; s++) {
            _springs.Add(SimulationRunner.InstantiateSpring(
                _rootGameObject.transform, springConstant, _points[s], _points[s+1]));
        }
    }
}

public class Plane : SimObject {
    public Plane(int width = 5, int height = 5, float pointSpacing = 0.5f, float pointMass = 1, float springConstant = 5) 
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
                    _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + 1]));
                
                _springs.Add(SimulationRunner.InstantiateSpring(
                    _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + width]));
                
                _springs.Add(SimulationRunner.InstantiateSpring(
                    _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + width + 1]));
            }
        }
    }
}

public class Cube : SimObject {
    public Cube(int length = 5, int width = 5, int height = 5, float pointSpacing = 0.5f, float pointMass = 1, 
        float springConstant = 5)
        : base($"Cube ({length}, {width}, {height})") {

        for (int p = 0; p < length; p++) {
            for (int p1 = 0; p1 < width; p1++) {
                for (int p2 = 0; p2 < height; p2++) {
                    _points.Add(SimulationRunner.InstantiatePoint(
                        Vector3.up * (pointSpacing * p) + Vector3.right * (pointSpacing * p1)
                                                        + Vector3.forward * (pointSpacing * p2),
                        _rootGameObject.transform, pointMass));
                }
            }
        }

        for (int s = 0; s < length - 1; s++) {
            for (int s1 = 0; s1 < width - 1; s1++) {
                for (int s2 = 0; s2 < height - 1; s2++) {
                    
                    int startIndex = s2 + (height * s1) + (height*width * s);

                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + 1]));

                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + length]));
                    
                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + length + 1]));
                    
                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + length*width]));
                    
                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex], _points[startIndex + length*width + 1]));

                    _springs.Add(SimulationRunner.InstantiateSpring(
                        _rootGameObject.transform, springConstant, _points[startIndex],
                        _points[startIndex + width + 1]));
                }
            }
        }
    }
}