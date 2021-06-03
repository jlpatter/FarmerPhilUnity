using UnityEngine;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool isShop;
        public SpawnerBehavior spawnerBehavior;
        public GrubArmyBehavior grubArmyBehavior;

        public void ContinueGame() {
            SetIsShop(false);
            grubArmyBehavior.numOfGrubs = 5;
            spawnerBehavior.SpawnGrubs();
        }

        public void SetIsShop(bool isShopValue) {
            isShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
