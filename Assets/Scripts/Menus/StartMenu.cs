using UnityEngine;

namespace Menus {
    public class StartMenu : MonoBehaviour {
        public bool isStart;

        private void Start() {
            isStart = true;
        }

        public void ContinueGame() {
            isStart = false;
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
