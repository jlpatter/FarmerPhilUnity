using UnityEngine;
using UnityEngine.UI;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool IsShop { get; private set; }
        
        public GameObject player;
        public GameObject sprayCanPrefab;
        public GameManagerBehavior gameManagerBehavior;
        public SpawnerBehavior spawnerBehavior;
        public GrubArmyBehavior grubArmyBehavior;

        private PlayerBehavior _playerBehavior;
        private Text _waveCounterText;

        private const float SprayCanOffset = 0.2f;

        private void Start() {
            _playerBehavior = player.GetComponent<PlayerBehavior>();
            _waveCounterText = GameObject.Find("WaveCounter").transform.Find("Counter").gameObject.GetComponent<Text>();
        }

        public void ContinueGame() {
            SetIsShop(false);
            gameManagerBehavior.CurrentWave++;
            _waveCounterText.text = gameManagerBehavior.CurrentWave.ToString();
            grubArmyBehavior.NumOfGrubs += 2;
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
            IsShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
