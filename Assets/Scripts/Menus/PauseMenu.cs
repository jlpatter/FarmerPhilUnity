using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus {
    public class PauseMenu : MonoBehaviour {
        public bool IsPaused { get; set; }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void QuitGame() {
            Application.Quit();
        }
    }
}
