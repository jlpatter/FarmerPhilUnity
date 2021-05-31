using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private const float Speed = 20.0f;

    // Start is called before the first frame update
    private void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
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
