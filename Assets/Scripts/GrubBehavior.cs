using Menus;
using UnityEngine;

public class GrubBehavior : MonoBehaviour {

    public PlayerHealthBar healthBar;

    private float _health;
    private PauseMenu _pauseMenu;
    private StartMenu _startMenu;
    private ShopMenu _shopMenu;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private GameObject _playerGameObject;
    private GameObject _wheatField;
    private GameObject _grubArmy;
    private bool _hasNearbyWheat;
    private bool _isTouchingBat;
    private bool _isFirstDamage;
    private GameObject _nearbyWheat;
    
    private const float Speed = 10.0f;
    private const float DeadZone = 0.1f;

    // Start is called before the first frame update
    private void Start() {
        _health = 100.0f;
        _isFirstDamage = true;
        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
        _isTouchingBat = false;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _wheatField = GameObject.FindGameObjectWithTag("WheatField");
        _grubArmy = GameObject.Find("GrubArmy");
        _pauseMenu = GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().pauseMenu;
        _startMenu = GameObject.Find("StartMenu").GetComponent<StartMenu>();
        _shopMenu = GameObject.Find("ShopMenuManager").GetComponent<ShopMenuManager>().shopMenu;
    }

    // Update is called once per frame
    private void Update() {
        if (!_pauseMenu.isPaused && !_startMenu.isStart && !_shopMenu.isShop) {
            TakeDamage();
            
            if (IsNearPlayer()) {
                _hasNearbyWheat = false;
                FollowGameObject(_playerGameObject);
            }
            else {
                if ((!_hasNearbyWheat || _nearbyWheat == null) && _wheatField.transform.childCount != 0) {
                    _nearbyWheat = FindNearestWheat();
                    _hasNearbyWheat = true;
                }

                if (_wheatField.transform.childCount == 0) {
                    FollowGameObject(_playerGameObject);
                }
                else {
                    FollowGameObject(_nearbyWheat);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Bat")) {
            _isTouchingBat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name.Contains("Bat")) {
            _isTouchingBat = false;
        }
    }

    private void TakeDamage() {
        if (_isTouchingBat) {
            if (_isFirstDamage) {
                healthBar.gameObject.transform.parent.gameObject.SetActive(true);
                _isFirstDamage = false;
            }
            _health -= 0.3f;
            healthBar.SetHealth(_health);

            if (_health <= 0.0f) {
                if (_grubArmy.transform.childCount == 1) {
                    _shopMenu.SetIsShop(true);
                }
                Destroy(gameObject);
            }
        }
    }

    private bool IsNearPlayer() {
        var currentDistance = (transform.position - _playerGameObject.transform.position).magnitude;
        return currentDistance < 2.0f;
    }

    private GameObject FindNearestWheat() {
        var shortestDistance = 999999999f;
        var currentWheat = _wheatField.transform.GetChild(0).gameObject;
        for (var i = 0; i < _wheatField.transform.childCount; i++) {
            var currentDistance = (transform.position - _wheatField.transform.GetChild(i).position).magnitude;
            if (currentDistance < shortestDistance) {
                shortestDistance = currentDistance;
                currentWheat = _wheatField.transform.GetChild(i).gameObject;
            }
        }
        return currentWheat;
    }

    private void FollowGameObject(GameObject gameObjectToTravelTo) {
        var travelVector = new Vector2(_rigidbody2D.position.x, _rigidbody2D.position.y);
        var isMoving = false;
        if (gameObjectToTravelTo.transform.position.x - DeadZone > travelVector.x) {
            travelVector.x += Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 1);
            _spriteRenderer.flipX = false;
            isMoving = true;
        }
        
        if (gameObjectToTravelTo.transform.position.x + DeadZone < travelVector.x) {
            travelVector.x -= Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 1);
            _spriteRenderer.flipX = true;
            isMoving = true;
        }
        
        if (gameObjectToTravelTo.transform.position.y - DeadZone > travelVector.y) {
            travelVector.y += Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 2);
            _spriteRenderer.flipX = false;
            isMoving = true;
        }
        
        if (gameObjectToTravelTo.transform.position.y + DeadZone < travelVector.y) {
            travelVector.y -= Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 3);
            _spriteRenderer.flipX = false;
            isMoving = true;
        }

        if (!isMoving) {
            _animator.SetInteger("MoveState", 0);
        }
        _rigidbody2D.MovePosition(travelVector);
    }
}
