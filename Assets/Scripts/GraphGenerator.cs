using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {
    public static GraphGenerator Instance;

    public Transform background;

    private void Awake() {
        Instance = this;
    }

    public static void CreateGraph(List<DataPoint> data) {
        var xMin = data.Min(d => d.xPos);
        var xMax = data.Max(d => d.xPos);
        var xSize = xMax - xMin;
        
        var yMin = data.Min(d => d.yPos);
        var yMax = data.Max(d => d.yPos);
        var ySize = yMax - yMin;
        
        //void 
    }

    public struct DataPoint {
        public float xPos;
        public float yPos;

        public DataPoint(float xPos, float yPos) {
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }
}