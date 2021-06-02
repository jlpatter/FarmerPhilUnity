using UnityEngine;

public class GrubBehavior : MonoBehaviour {
    private PauseMenu _pauseMenu;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameObject _playerGameObject;
    private GameObject _wheatField;
    private bool _hasNearbyWheat;
    private GameObject _nearbyWheat;
    
    private const float Speed = 1.0f;
    private const float DeadZone = 0.1f;

    // Start is called before the first frame update
    private void Start() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _wheatField = GameObject.FindGameObjectWithTag("WheatField");
        _pauseMenu = GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().pauseMenu;
    }

    // Update is called once per frame
    private void Update() {
        if (!_pauseMenu.isPaused) {
            if (IsNearPlayer()) {
                _hasNearbyWheat = false;
                FollowGameObject(_playerGameObject);
            }
            else {
                if (!_hasNearbyWheat) {
                    _nearbyWheat = FindNearestWheat();
                    _hasNearbyWheat = true;
                }
                FollowGameObject(_nearbyWheat);
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
        var travelVector = transform.position;
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
        transform.position = travelVector;
    }
}
