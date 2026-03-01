using UnityEngine;

public class PogoController : MonoBehaviour
{
    [SerializeField] private float _torqueForce; // Сила крутящего момента
    [SerializeField] private float _maxAngularVelocity; // Максимальная сила крутящего момента
    [SerializeField] private float _jumpForce; // Максимальная сила прыжка
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody.maxAngularVelocity = _maxAngularVelocity;
        _rigidbody.centerOfMass = Vector3.zero;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            Vector3 vector = new Vector3(0, 0, -horizontalInput * _torqueForce * Time.deltaTime);
            _rigidbody.AddTorque(vector,ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _groundChecker.CanJump)
        {
            Debug.Log("JUMP");
            _groundChecker.OnJump();
            _rigidbody.AddForce(transform.up * _jumpForce);
        }
    }
}
