using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public bool isPaused;
    
    public void QuitGame() {
        Application.Quit();
    }
}
