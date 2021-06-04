using UnityEngine;

namespace Menus {
    public class PauseMenu : MonoBehaviour {
        public bool IsPaused { get; set; }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
