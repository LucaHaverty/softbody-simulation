using System.Linq;
using TMPro;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {
    public static GraphGenerator Instance;

    [SerializeField] private Transform background;

    [Space] [Header("Axis Labels")] [SerializeField]
    private Transform xLabelsParent;

    [SerializeField] private Transform yLabelsParent;

    private TextMeshProUGUI[] xLabels;
    private TextMeshProUGUI[] yLabels;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        xLabels = new TextMeshProUGUI[xLabelsParent.childCount];
        yLabels = new TextMeshProUGUI[yLabelsParent.childCount];

        int i = 0;
        foreach (Transform t in xLabelsParent) {
            xLabels[i] = t.GetComponent<TextMeshProUGUI>();
            i++;
        }

        i = 0;
        foreach (Transform t in yLabelsParent) {
            yLabels[i] = t.GetComponent<TextMeshProUGUI>();
            i++;
        }
    }

    public void CreateGraph(params (Vector2[] points, Color color)[] allData) {
        Vector2[] combinedData = allData.SelectMany(i => i.points).ToArray();

        if (combinedData.Length == 0)
            return;

        var dataMin = new Vector2(combinedData.Min(d => d.x), combinedData.Min(d => d.y));
        var dataMax = new Vector2(combinedData.Max(d => d.x), combinedData.Max(d => d.y));

        dataMax.y = Mathf.Max(Mathf.Abs(dataMin.y), Mathf.Abs(dataMax.y));
        dataMin.y = -dataMax.y;

        var dataScale = dataMax - dataMin;

        ClearGraph();

        int layer = 1;
        foreach (var data in allData) {
            var points = data.points;

            if (points.Length == 0)
                continue;

            int pointFrequency = Mathf.CeilToInt(points.Length / (float)SimConfig.MaxGraphPoints);

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

        UpdateLabels(dataMin, dataMax);
    }

    private void UpdateLabels(Vector2 min, Vector2 max) {
        var step = (max - min) / new Vector2(xLabels.Length, yLabels.Length - 1);

        var i = 1;
        foreach (var label in xLabels) {
            label.text = Round(i * step.x + min.x).ToString();
            i++;
        }

        i = yLabels.Length - 1;
        foreach (var label in yLabels) {
            label.text = Round(i * step.y + min.y).ToString();
            i--;
        }
    }

    private float Round(float x) {
        return Mathf.Round(x * 100) / 100f;
    }

    private void ClearGraph() {
        foreach (Transform child in background)
            Destroy(child.gameObject);
    }
}