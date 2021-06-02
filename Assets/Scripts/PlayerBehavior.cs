using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehavior : MonoBehaviour {

    [FormerlySerializedAs("PauseCanvas")]
    public GameObject pauseCanvas;
    public PlayerHealthBar healthBar;
    public PauseMenu pauseMenu;

    private float _health;
    private bool _isTouchingGrubby;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private const float MaxHealth = 100.0f;
    private const float Speed = 20.0f;

    // Start is called before the first frame update
    private void Start() {
        _health = MaxHealth;
        _isTouchingGrubby = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        ShowPauseMenu();
        if (!pauseMenu.isPaused) {
            TakeDamage();
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
            _health -= 0.1f;
            healthBar.SetHealth(_health);
        }
    }

    private void MovePlayer() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _spriteRenderer.flipX = true;
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            _spriteRenderer.flipX = false;
        }
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime));
    }
}
