using UnityEngine;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool isShop;

        private void Start() {
            isShop = false;
            transform.parent.parent.gameObject.SetActive(false);
        }

        public void SetIsShop(bool isShopValue) {
            isShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
