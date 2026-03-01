using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpCooldown = 0.1f;
    public bool isGrounded = false;
    public bool CanJump => isGrounded || (Time.time - _lastGroundTime <= _coyoteTime) && (Time.time - _lastJumpTime >= _jumpCooldown); // + COOLDOWN


    public System.Action OutOfGround;
    public System.Action Landing;
    private float _lastGroundTime;
    private float _lastJumpTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            isGrounded = true;
            _lastGroundTime = Time.time; // Запоминаем время контакта
            Landing?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            isGrounded = false;
            OutOfGround?.Invoke();
            // _lastGroundTime НЕ сбрасывается — coyote работает!
        }
    }

    public void OnJump()
    {
        _lastJumpTime = Time.time;
    }
}
