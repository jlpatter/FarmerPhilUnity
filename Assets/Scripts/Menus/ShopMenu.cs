using UnityEngine;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool isShop;
        public GameObject player;
        public GameObject sprayCanPrefab;
        public SpawnerBehavior spawnerBehavior;
        public GrubArmyBehavior grubArmyBehavior;

        private PlayerBehavior _playerBehavior;

        private void Start() {
            _playerBehavior = player.GetComponent<PlayerBehavior>();
        }

        public void ContinueGame() {
            SetIsShop(false);
            grubArmyBehavior.numOfGrubs = 5;
            spawnerBehavior.SpawnGrubs();
        }

        public void PurchaseSprayCan() {
            var newSprayCan = Instantiate(sprayCanPrefab, new Vector3(player.transform.position.x, player.transform.position.y), Quaternion.identity, player.transform);
            newSprayCan.SetActive(false);
            _playerBehavior.AddWeapon(newSprayCan);
        }

        public void SetIsShop(bool isShopValue) {
            isShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
