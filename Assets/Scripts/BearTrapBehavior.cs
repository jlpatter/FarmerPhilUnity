using Menus;
using UnityEngine;

public class BearTrapBehavior : MonoBehaviour {

    public GameObject sprungBearTrapPrefab;

    private GrubArmyBehavior _grubArmyBehavior;
    private ShopMenu _shopMenu;

    private void Start() {
        _grubArmyBehavior = GameObject.Find("GrubArmy").GetComponent<GrubArmyBehavior>();
        _shopMenu = GameObject.Find("ShopMenuManager").GetComponent<ShopMenuManager>().shopMenu;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grub")) {
            if (_grubArmyBehavior.NumOfGrubs == 1) {
                _shopMenu.SetIsShop(true);
            }
            _grubArmyBehavior.NumOfGrubs--;
            Destroy(other.gameObject);
            Instantiate(sprungBearTrapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
