using UnityEngine;

public class GrubBehavior : MonoBehaviour {
    private Animator _animator;
    private const float Speed = 2.0f;
    
    // Start is called before the first frame update
    private void Start() {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {
        _animator.SetInteger("MoveState", 0);
        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
        _animator.SetFloat("Speed", Speed * Time.deltaTime);
    }
}
