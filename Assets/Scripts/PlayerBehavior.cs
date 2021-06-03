using Menus;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehavior : MonoBehaviour {

    [FormerlySerializedAs("PauseCanvas")]
    public GameObject pauseCanvas;
    public GameObject bat;
    public GameObject sprayCan;
    public PlayerHealthBar healthBar;
    public PauseMenu pauseMenu;
    public StartMenu startMenu;
    public ShopMenu shopMenu;

    private float _health;
    private int _currentWeaponIndex;
    private bool _isTouchingGrubby;
    private bool _hasGoneRight;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _batSpriteRenderer;
    private SpriteRenderer _sprayCanSpriteRenderer;

    private const float MaxHealth = 100.0f;
    private const float GrubStrength = 0.05f;
    private const float Speed = 5.0f;

    // Start is called before the first frame update
    private void Start() {
        _health = MaxHealth;
        _currentWeaponIndex = 0;
        _isTouchingGrubby = false;
        _hasGoneRight = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _batSpriteRenderer = bat.GetComponent<SpriteRenderer>();
        _sprayCanSpriteRenderer = sprayCan.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        ShowPauseMenu();
        if (!pauseMenu.isPaused && !startMenu.isStart && !shopMenu.isShop) {
            TakeDamage();
            SwitchWeapon();
            if (gameObject.transform.GetChild(_currentWeaponIndex).name.Equals("Bat")) {
                sprayCan.SetActive(false);
                SwingBat();
            }
            else if (gameObject.transform.GetChild(_currentWeaponIndex).name.Equals("SprayCan")) {
                bat.SetActive(false);
                SprayCan();
            }
            MovePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grubby")) {
            _isTouchingGrubby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name.Contains("Grubby")) {
            _isTouchingGrubby = false;
        }
    }

    private void ShowPauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            pauseMenu.isPaused = !pauseMenu.isPaused;
        }
    }

    private void TakeDamage() {
        if (_isTouchingGrubby) {
            _health -= GrubStrength;
            healthBar.SetHealth(_health);
        }
    }

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.E)) {
            _currentWeaponIndex++;
            if (_currentWeaponIndex >= gameObject.transform.childCount) {
                _currentWeaponIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            _currentWeaponIndex--;
            if (_currentWeaponIndex < 0) {
                _currentWeaponIndex = gameObject.transform.childCount - 1;
            }
        }
    }

    private void SwingBat() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            bat.SetActive(true);
        }
    }

    private void SprayCan() {
        if (Input.GetKey(KeyCode.Space)) {
            sprayCan.SetActive(true);
        }
        else {
            sprayCan.SetActive(false);
        }
    }

    private void MovePlayer() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _spriteRenderer.flipX = true;
            _batSpriteRenderer.flipX = true;
            _sprayCanSpriteRenderer.flipX = true;
            if (!_hasGoneRight) {
                bat.transform.localPosition = new Vector3(-bat.transform.localPosition.x, bat.transform.localPosition.y, bat.transform.localPosition.z);
                sprayCan.transform.localPosition = new Vector3(-sprayCan.transform.localPosition.x, sprayCan.transform.localPosition.y, sprayCan.transform.localPosition.z);
                _hasGoneRight = true;
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            _spriteRenderer.flipX = false;
            _batSpriteRenderer.flipX = false;
            _sprayCanSpriteRenderer.flipX = false;
            if (_hasGoneRight) {
                bat.transform.localPosition = new Vector3(-bat.transform.localPosition.x, bat.transform.localPosition.y, bat.transform.localPosition.z);
                sprayCan.transform.localPosition = new Vector3(-sprayCan.transform.localPosition.x, sprayCan.transform.localPosition.y, sprayCan.transform.localPosition.z);
                _hasGoneRight = false;
            }
        }
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime);
    }
}
