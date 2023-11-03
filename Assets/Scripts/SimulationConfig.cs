using UnityEngine;

public class SimulationConfig : MonoBehaviour {
    private static SimulationConfig Instance;

    private void Awake() { Instance = this; }

    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _springPrefab;
    [SerializeField] private float _forceOfGravity;
    [SerializeField] private float _floorLevel;

    public static float ForceOfGravity => Instance._forceOfGravity;
    public static GameObject PointPrefab => Instance._pointPrefab;
    public static GameObject SpringPrefab => Instance._springPrefab;
    public static float FloorLevel => Instance._floorLevel;
}