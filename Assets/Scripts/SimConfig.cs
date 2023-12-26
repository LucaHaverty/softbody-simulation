using ConfigInstances;
using UnityEngine;

public class SimConfig : MonoBehaviour {
    private static SimConfig Instance;

    private void Awake() {
        Config = _config;
        Instance = this;
    }

    [SerializeField] private ConfigInstance _config;
    [SerializeField] private Transform _sim;
    [SerializeField] private Transform _graph;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _springPrefab;
    [SerializeField] private GameObject _graphPoint;

    private static ConfigInstance Config;

    public static Transform Sim => Instance._sim;
    public static Transform Graph => Instance._graph;
    public static GameObject GraphPoint => Instance._graphPoint;
    public static float ForceOfGravity => Config._forceOfGravity;
    public static GameObject PointPrefab => Instance._pointPrefab;
    public static GameObject SpringPrefab => Instance._springPrefab;
    public static float StartHeight => Config._startHeight;
    public static float SpringConstant => Config._springConstant;
    public static float DampingConstant => Config._dampingConstant;
    public static float PointMass => Config._pointMass;
    public static int MaxGraphPoints => Config._maxGraphPoints;
}