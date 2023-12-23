using System;
using System.Linq;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {
    public static GraphGenerator Instance;
    [SerializeField] private Transform background;

    private void Awake() {
        Instance = this;
    }

    public void CreateGraph(params (Vector2[] points, Color color)[] allData) {
        ClearGraph();

        int layer = 1;
        foreach (var data in allData) {
            var points = data.points;

            if (points.Length == 0)
                continue;

            var dataMin = new Vector2(points.Min(d => d.x), points.Min(d => d.y));
            var dataMax = new Vector2(points.Max(d => d.x), points.Max(d => d.y));
            var dataScale = dataMax - dataMin;

            int pointFrequency =  Mathf.CeilToInt(points.Length / (float)SimConfig.MaxGraphPoints);

            for (int i = 0; i < points.Length; i++) {
                
                if (i % pointFrequency != 0)
                    continue;
                
                var p = points[i];
                
                Vector2 normalizedDataPos = (p - dataMin) / dataScale;
                Vector2 localPosition = normalizedDataPos - new Vector2(0.5f, 0.5f);

                if (float.IsNaN(localPosition.x) || float.IsNaN(localPosition.y))
                    return;

                var pointTrf = Instantiate(SimConfig.GraphPoint).transform;
                pointTrf.parent = background;
                pointTrf.GetComponent<SpriteRenderer>().color = data.color;
                pointTrf.GetComponent<SpriteRenderer>().sortingOrder = layer;

                pointTrf.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            }

            layer++;
        }
    }

    private void ClearGraph() {
        foreach (Transform child in background) {
            Destroy(child.gameObject);
        }
        /*while(background.childCount > 0)
            DestroyImmediate(background.GetChild(0));*/
    }
}