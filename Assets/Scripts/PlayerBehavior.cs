using System;
using System.Collections.Generic;
using System.Linq;
using Menus;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
    public int Money { get; set; }
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    public GameObject bat;
    public GameObject bearTrapPrefab;
    public GameObject gameOverCanvas;
    public PlayerHealthBar healthBar;
    public PauseMenu pauseMenu;
    public StartMenu startMenu;
    public ShopMenu shopMenu;
    public Text currentWeaponText;
    
    private int _currentWeaponIndex;
    private bool _isTouchingGrubby;
    private bool _hasGoneRight;
    private List<Tuple<GameObject, SpriteRenderer>> _weapons;
    private SpriteRenderer _spriteRenderer;
    
    private const float GrubStrength = 0.05f;
    private const float Speed = 5.0f;

    // Start is called before the first frame update
    private void Start() {
        MaxHealth = 100.0f;
        Health = MaxHealth;
        Money = 0;
        _currentWeaponIndex = 0;
        _isTouchingGrubby = false;
        _hasGoneRight = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weapons = new List<Tuple<GameObject, SpriteRenderer>>();
        AddWeapon(bat);
    }

    // Update is called once per frame
    private void Update() {
        if (!pauseMenu.IsPaused && !startMenu.IsStart && !shopMenu.IsShop) {
            TakeDamage();
            SwitchWeapon();
            foreach (var (weapon, _) in _weapons) {
                if (!weapon.name.Contains(_weapons[_currentWeaponIndex].Item1.name)) {
                    weapon.SetActive(false);
                }
            }
            if (_weapons[_currentWeaponIndex].Item1.name.Contains("Bat")) {
                SwingBat();
            }
            else if (_weapons[_currentWeaponIndex].Item1.name.Contains("SprayCan")) {
                SprayCan();
            }
            else if (_weapons[_currentWeaponIndex].Item1.name.Contains("BearTrapHold")) {
                _weapons[_currentWeaponIndex].Item1.SetActive(true);
                PlantBearTrap();
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

    public bool HasWeapon(string weaponName) {
        var hasWeapon = false;
        foreach (var _ in _weapons.Where(weapon => weapon.Item1.name.Contains(weaponName))) {
            hasWeapon = true;
        }

        return hasWeapon;
    }
    
    public void SetPlayerHealthToMax() {
        Health = MaxHealth;
        healthBar.SetHealth(Health);
    }

    private void TakeDamage() {
        if (_isTouchingGrubby) {
            Health -= GrubStrength;
            healthBar.SetHealth(Health);

            if (Health <= 0.0f) {
                gameOverCanvas.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void SwitchWeapon() {
        var changedWeapon = false;
        if (Input.GetKeyDown(KeyCode.E)) {
            changedWeapon = true;
            _currentWeaponIndex++;
            if (_currentWeaponIndex >= _weapons.Count) {
                _currentWeaponIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            changedWeapon = true;
            _currentWeaponIndex--;
            if (_currentWeaponIndex < 0) {
                _currentWeaponIndex = _weapons.Count - 1;
            }
        }

        if (changedWeapon) {
            currentWeaponText.text = _weapons[_currentWeaponIndex].Item1.tag;
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

    private void PlantBearTrap() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_spriteRenderer.flipX) {
                Instantiate(bearTrapPrefab, new Vector3(transform.position.x + bearTrapPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, transform.position.y, -1.0f), Quaternion.identity);
            }
            else {
                Instantiate(bearTrapPrefab, new Vector3(transform.position.x - bearTrapPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, transform.position.y, -1.0f), Quaternion.identity);
            }
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
