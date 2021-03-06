using Menus;
using UnityEngine;

public class WheatBehavior : MonoBehaviour {
    
    public bool IsTarget { get; set; }

    public PlayerHealthBar healthBar;

    private float _health;
    private bool _isTouchingGrubby;
    private bool _isFirstDamage;
    private PauseMenu _pauseMenu;
    private StartMenu _startMenu;
    private ShopMenu _shopMenu;

    private const float MaxHealth = 100.0f;
    
    // Start is called before the first frame update
    private void Start() {
        IsTarget = false;
        _health = MaxHealth;
        _isTouchingGrubby = false;
        _isFirstDamage = true;
        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
        _pauseMenu = GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().pauseMenu;
        _startMenu = GameObject.Find("StartMenu").GetComponent<StartMenu>();
        _shopMenu = GameObject.Find("ShopMenuManager").GetComponent<ShopMenuManager>().shopMenu;
    }

    // Update is called once per frame
    private void Update() {
        if (!_pauseMenu.IsPaused && !_startMenu.IsStart && !_shopMenu.IsShop) {
            if (_isTouchingGrubby && IsTarget) {
                if (_isFirstDamage) {
                    healthBar.gameObject.transform.parent.gameObject.SetActive(true);
                    _isFirstDamage = false;
                }
            
                _health -= 0.1f;
                healthBar.SetHealth(_health);

                if (_health <= 0.0f) {
                    Destroy(gameObject);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grubby")) {
            _isTouchingGrubby = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grubby")) {
            _isTouchingGrubby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grubby")) {
            _isTouchingGrubby = false;
        }
    }
}
