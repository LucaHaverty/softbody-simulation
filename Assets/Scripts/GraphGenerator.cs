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

        foreach (var data in allData) {
            var points = data.points;

            if (points.Length == 0)
                continue;

            var dataMin = new Vector2(points.Min(d => d.x), points.Min(d => d.y));
            var dataMax = new Vector2(points.Max(d => d.x), points.Max(d => d.y));
            var dataScale = dataMax - dataMin;

            foreach (var d in points) {
                Vector2 normalizedDataPos = (d - dataMin) / dataScale;
                Vector2 localPosition = normalizedDataPos - new Vector2(0.5f, 0.5f);

                var pointTrf = Instantiate(SimConfig.GraphPoint).transform;
                pointTrf.parent = background;
                pointTrf.GetComponent<SpriteRenderer>().color = data.color;

                pointTrf.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            }
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