using UnityEngine;

public class WheatBehavior : MonoBehaviour {

    public PlayerHealthBar healthBar;

    private float _health;

    private const float MaxHealth = 100.0f;
    
    // Start is called before the first frame update
    private void Start() {
        _health = MaxHealth;
        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _health -= 10.0f;
            healthBar.gameObject.transform.parent.gameObject.SetActive(true);
            healthBar.SetHealth(_health);
        }
    }
}
