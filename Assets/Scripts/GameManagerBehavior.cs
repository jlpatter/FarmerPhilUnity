using Menus;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour {
    public int CurrentWave { get; set; }

    public PauseMenu pauseMenu;

    private GameObject _pauseCanvas;

    private void Start() {
        CurrentWave = 1;
        _pauseCanvas = pauseMenu.gameObject.transform.parent.parent.gameObject;
    }

    private void Update() {
        ShowPauseMenu();
    }

    private void ShowPauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
            pauseMenu.IsPaused = !pauseMenu.IsPaused;
        }
    }
}
