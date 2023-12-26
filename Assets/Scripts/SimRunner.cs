using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimRunner : MonoBehaviour {
    public static SimRunner Instance;

    private readonly List<SimObject> _simObjects = new();

    private bool _simRunning;
    public bool SimRunning => _simRunning;

    private readonly List<(float time, bool hasHitGround, Vector3 pos, Vector3 vel, Vector3 acc)> _states = new();

    public bool HasCollidedWithFloor;

    private float simStartTime;
    private float simPauseTime;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        CreateObjects();
    }


    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            PlayPauseSim();
        }

        if (Input.GetKeyDown(KeyCode.R)) ResetSim();*/

        GraphStates();
    }

    private void GraphStates() {
        var posPoints = _states.Select(s => new Vector2(s.time, s.pos.y)).ToArray();
        var velPoints = _states.Select(s => new Vector2(s.time, s.vel.y)).ToArray();
        var accPoints = _states.Select(s => new Vector2(s.time, s.acc.y)).ToArray();

        GraphGenerator.Instance.CreateGraph((posPoints, Color.red),
            (velPoints, Color.green) /*, (accPoints, Color.blue)*/);
    }

    private void CalcBounceHeight() {
        Debug.Log(_states.Where(s => s.hasHitGround).Max(s => s.pos.y));
    }

    private void FixedUpdate() {
        if (_simRunning)
            RunSim();
    }

    private void RunSim() {
        _simObjects.ForEach(s => { s.Update(); });

        //if (HasCollidedWithFloor)
        _states.Add((Time.time - simStartTime, HasCollidedWithFloor, _simObjects[0].EstimatedPosition,
            _simObjects[0].EstimatedVelocity, _simObjects[0].EstimatedAcceleration));
    }
    
    public void PlayPauseSim() {
        _simRunning = !_simRunning;

        if (!_simRunning) {
            simPauseTime = Time.time;
        }
        else {
            simStartTime += Time.time - simPauseTime;
        }
    }

    public void ResetSim() {
        DestroyObjects();
        CreateObjects();
        _states.Clear();
        HasCollidedWithFloor = false;
        simStartTime = Time.time;
        _simRunning = true;
    }

    private void CreateObjects() {
        _simObjects.Add(new Cube(5, 5, 5, 0.5f));
    }

    private void DestroyObjects() {
        _simObjects.ForEach(s => { s.DestroyRootObject(); });

        _simObjects.Clear();
    }

    public static PointMass InstantiatePoint(Vector3 position, Transform parent) {
        var point = Instantiate(SimConfig.PointPrefab, position + parent.transform.position, Quaternion.identity,
                parent)
            .GetComponent<PointMass>();

        return point;
    }

    public static Spring InstantiateSpring(Transform parent, PointMass refA, PointMass refB) {

        var spring = Instantiate(SimConfig.SpringPrefab,
            (refA.transform.position + refB.transform.position) / 2,
            Quaternion.identity, parent).GetComponent<Spring>();

        spring.SetRefs(refA, refB);
        return spring;
    }
}
