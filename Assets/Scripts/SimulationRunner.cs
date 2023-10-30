using System.Collections.Generic;
using UnityEngine;

public class SimulationRunner : MonoBehaviour {
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _springPrefab;

    private List<Point> _points;
    private List<Spring> _springs;

    private void CreateObject() {
        
    }
}
