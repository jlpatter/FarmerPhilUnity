using System;
using System.Collections.Generic;
using Menus;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehavior : MonoBehaviour {

    [FormerlySerializedAs("PauseCanvas")]
    public GameObject pauseCanvas;
    public GameObject bat;
    public PlayerHealthBar healthBar;
    public PauseMenu pauseMenu;
    public StartMenu startMenu;
    public ShopMenu shopMenu;

    private float _health;
    private int _currentWeaponIndex;
    private bool _isTouchingGrubby;
    private bool _hasGoneRight;
    private List<Tuple<GameObject, SpriteRenderer>> _weapons;
    private SpriteRenderer _spriteRenderer;

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
        _weapons = new List<Tuple<GameObject, SpriteRenderer>>();
        AddWeapon(bat);
    }

    // Update is called once per frame
    private void Update() {
        ShowPauseMenu();
        if (!pauseMenu.isPaused && !startMenu.isStart && !shopMenu.isShop) {
            TakeDamage();
            SwitchWeapon();
            if (_weapons[_currentWeaponIndex].Item1.name.Contains("Bat")) {
                foreach (var (weapon, _) in _weapons) {
                    if (!weapon.name.Contains("Bat")) {
                        weapon.SetActive(false);
                    }
                }
                SwingBat();
            }
            else if (_weapons[_currentWeaponIndex].Item1.name.Contains("SprayCan")) {
                foreach (var (weapon, _) in _weapons) {
                    if (!weapon.name.Contains("SprayCan")) {
                        weapon.SetActive(false);
                    }
                }
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

    public void AddWeapon(GameObject w) {
        _weapons.Add(new Tuple<GameObject, SpriteRenderer>(w, w.GetComponent<SpriteRenderer>()));
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
            if (_currentWeaponIndex >= _weapons.Count) {
                _currentWeaponIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            _currentWeaponIndex--;
            if (_currentWeaponIndex < 0) {
                _currentWeaponIndex = _weapons.Count - 1;
            }
        }
    }

    private void SwingBat() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _weapons[_currentWeaponIndex].Item1.SetActive(true);
        }
    }

    private void SprayCan() {
        if (Input.GetKey(KeyCode.Space)) {
            _weapons[_currentWeaponIndex].Item1.SetActive(true);
        }
        else {
            _weapons[_currentWeaponIndex].Item1.SetActive(false);
        }
    }

    private void MovePlayer() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _spriteRenderer.flipX = true;
            foreach (var weapon in _weapons) {
                weapon.Item2.flipX = true;
            }
            if (!_hasGoneRight) {
                foreach (var (weapon, _) in _weapons) {
                    weapon.transform.localPosition = new Vector3(-weapon.transform.localPosition.x, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
                }
                _hasGoneRight = true;
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            _spriteRenderer.flipX = false;
            foreach (var weapon in _weapons) {
                weapon.Item2.flipX = false;
            }
            if (_hasGoneRight) {
                foreach (var (weapon, _) in _weapons) {
                    weapon.transform.localPosition = new Vector3(-weapon.transform.localPosition.x, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
                }
                _hasGoneRight = false;
            }
        }
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime);
    }
}
