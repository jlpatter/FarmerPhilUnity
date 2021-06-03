using UnityEngine;

namespace Menus {
    public class PauseMenu : MonoBehaviour {
        public bool isPaused;

        public void QuitGame() {
            Application.Quit();
        }
    }
}
