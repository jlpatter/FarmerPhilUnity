using UnityEngine;

public class GrubBehavior : MonoBehaviour {
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameObject _playerGameObject;
    
    private const float Speed = 2.0f;
    private const float DeadZone = 0.1f;

    // Start is called before the first frame update
    private void Start() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update() {
        var travelVector = transform.position;
        var isMoving = false;
        if (_playerGameObject.transform.position.x - DeadZone > travelVector.x) {
            travelVector.x += Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 1);
            _spriteRenderer.flipX = false;
            isMoving = true;
        }
        
        if (_playerGameObject.transform.position.x + DeadZone < travelVector.x) {
            travelVector.x -= Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 1);
            _spriteRenderer.flipX = true;
            isMoving = true;
        }
        
        if (_playerGameObject.transform.position.y - DeadZone > travelVector.y) {
            travelVector.y += Speed * Time.deltaTime;
            _animator.SetInteger("MoveState", 2);
            _spriteRenderer.flipX = false;
            isMoving = true;
        }
        
        if (_playerGameObject.transform.position.y + DeadZone < travelVector.y) {
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
