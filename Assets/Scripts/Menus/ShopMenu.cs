using UnityEngine;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool isShop;
        public GameObject player;
        public GameObject sprayCanPrefab;
        public SpawnerBehavior spawnerBehavior;
        public GrubArmyBehavior grubArmyBehavior;

        private PlayerBehavior _playerBehavior;

        private const float SprayCanOffset = 0.2f;

        private void Start() {
            _playerBehavior = player.GetComponent<PlayerBehavior>();
        }

        public void ContinueGame() {
            SetIsShop(false);
            grubArmyBehavior.numOfGrubs = 5;
            spawnerBehavior.SpawnGrubs();
        }

        public void PurchaseSprayCan() {
            if (!_playerBehavior.HasWeapon("SprayCan")) {
                GameObject newSprayCan;
                if (player.GetComponent<SpriteRenderer>().flipX) {
                    newSprayCan = Instantiate(sprayCanPrefab, new Vector3(player.transform.position.x + sprayCanPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f + SprayCanOffset, player.transform.position.y), Quaternion.identity, player.transform);
                    newSprayCan.GetComponent<SpriteRenderer>().flipX = true;
                }
                else {
                    newSprayCan = Instantiate(sprayCanPrefab, new Vector3(player.transform.position.x - sprayCanPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f - SprayCanOffset, player.transform.position.y), Quaternion.identity, player.transform);
                }
                newSprayCan.SetActive(false);
                _playerBehavior.AddWeapon(newSprayCan);
            }
        }

        public void SetIsShop(bool isShopValue) {
            isShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
