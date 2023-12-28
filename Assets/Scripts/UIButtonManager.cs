using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour {
    [SerializeField] private Button playPauseButton;
    [SerializeField] private Button resetButton;

    [SerializeField]private TextMeshProUGUI playPauseText;
    
    private void Awake() {
        playPauseButton.onClick.AddListener(PlayPausePressed);
        resetButton.onClick.AddListener(ResetPressed);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayPausePressed();
        if (Input.GetKeyDown(KeyCode.R))
            ResetPressed();
    }

    private void PlayPausePressed() {
        SimRunner.Instance.PlayPauseSim();
        playPauseText.text = SimRunner.Instance.SimRunning ? "Pause" : "Play";
    }

    private void ResetPressed() {
        SimRunner.Instance.ResetSim();
        playPauseText.text = "Pause";
    }
}
