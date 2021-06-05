using UnityEngine;
using UnityEngine.UI;

namespace Menus {
    public class ShopMenu : MonoBehaviour {
        public bool IsShop { get; private set; }
        
        public GameObject player;
        public GameObject sprungBearTraps;
        public GameObject sprayCanPrefab;
        public GameObject bearTrapHoldPrefab;
        public GameManagerBehavior gameManagerBehavior;
        public SpawnerBehavior spawnerBehavior;
        public GrubArmyBehavior grubArmyBehavior;
        public Text moneyText;

        private PlayerBehavior _playerBehavior;
        private Text _waveCounterText;

        private const float SprayCanOffset = 0.2f;
        private const int SprayCanCost = 5;
        private const int BearTrapCost = 20;
        private const int HealthCost = 50;

        private void Start() {
            _playerBehavior = player.GetComponent<PlayerBehavior>();
            _waveCounterText = GameObject.Find("WaveCounter").transform.Find("Counter").gameObject.GetComponent<Text>();
        }

        public void ContinueGame() {
            SetIsShop(false);
            gameManagerBehavior.CurrentWave++;
            _waveCounterText.text = gameManagerBehavior.CurrentWave.ToString();

            foreach (Transform sprungBearTrapTransform in sprungBearTraps.transform) {
                Destroy(sprungBearTrapTransform.gameObject);
            }

            grubArmyBehavior.NumOfGrubsForWave += 2;
            spawnerBehavior.SpawnGrubs();
        }

        public void PurchaseSprayCan() {
            if (!_playerBehavior.HasWeapon("SprayCan") && _playerBehavior.Money >= SprayCanCost) {
                GameObject newSprayCan;
                if (player.GetComponent<SpriteRenderer>().flipX) {
                    newSprayCan = Instantiate(sprayCanPrefab, new Vector3(player.transform.position.x + sprayCanPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f + SprayCanOffset, player.transform.position.y, -1.0f), Quaternion.identity, player.transform);
                    newSprayCan.GetComponent<SpriteRenderer>().flipX = true;
                }
                else {
                    newSprayCan = Instantiate(sprayCanPrefab, new Vector3(player.transform.position.x - sprayCanPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f - SprayCanOffset, player.transform.position.y, -1.0f), Quaternion.identity, player.transform);
                }
                newSprayCan.SetActive(false);
                _playerBehavior.AddWeapon(newSprayCan);
                _playerBehavior.Money -= SprayCanCost;
                moneyText.text = "$" + _playerBehavior.Money;
            }
        }

        public void PurchaseBearTraps() {
            if (!_playerBehavior.HasWeapon("BearTrapHold") && _playerBehavior.Money >= BearTrapCost) {
                GameObject newBearTrapHold;
                if (player.GetComponent<SpriteRenderer>().flipX) {
                    newBearTrapHold = Instantiate(bearTrapHoldPrefab, new Vector3(player.transform.position.x + bearTrapHoldPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, player.transform.position.y, -1.0f), Quaternion.identity, player.transform);
                }
                else {
                    newBearTrapHold = Instantiate(bearTrapHoldPrefab, new Vector3(player.transform.position.x - bearTrapHoldPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, player.transform.position.y, -1.0f), Quaternion.identity, player.transform);
                }
                newBearTrapHold.SetActive(false);
                _playerBehavior.AddWeapon(newBearTrapHold);
                _playerBehavior.Money -= BearTrapCost;
                moneyText.text = "$" + _playerBehavior.Money;
            }
        }

        public void PurchaseHealth() {
            if (_playerBehavior.Money >= HealthCost && _playerBehavior.Health < _playerBehavior.MaxHealth) {
                _playerBehavior.SetPlayerHealthToMax();
                _playerBehavior.Money -= HealthCost;
                moneyText.text = "$" + _playerBehavior.Money;
            }
        }

        public void SetIsShop(bool isShopValue) {
            IsShop = isShopValue;
            transform.parent.parent.gameObject.SetActive(isShopValue);
        }
    }
}
