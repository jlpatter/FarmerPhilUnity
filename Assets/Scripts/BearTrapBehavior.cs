using UnityEngine;

public class BearTrapBehavior : MonoBehaviour {

    public GameObject sprungBearTrapPrefab;

    private GrubArmyBehavior _grubArmyBehavior;
    private GameManagerBehavior _gameManagerBehavior;
    private Transform _sprungBearTrapsTransform;

    private void Start() {
        _grubArmyBehavior = GameObject.Find("GrubArmy").GetComponent<GrubArmyBehavior>();
        _gameManagerBehavior = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        _sprungBearTrapsTransform = GameObject.Find("SprungBearTraps").transform;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grub")) {
            if (_grubArmyBehavior.NumOfGrubs == 1) {
                _gameManagerBehavior.EndWave();
            }
            _grubArmyBehavior.NumOfGrubs--;
            Destroy(other.gameObject);
            Instantiate(sprungBearTrapPrefab, transform.position, Quaternion.identity, _sprungBearTrapsTransform);
            Destroy(gameObject);
        }
    }
}
