using UnityEngine;

namespace Menus {
    public class StartMenu : MonoBehaviour {
        public bool IsStart { get; private set; }

        private void Start() {
            IsStart = true;
        }

        public void ContinueGame() {
            IsStart = false;
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
