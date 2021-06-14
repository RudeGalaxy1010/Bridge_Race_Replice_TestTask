using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _gravityMultyplier = -9.81f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Animator _animator;

    private CharacterController _controller;
    private Vector3 _gravityVelocity;
    private bool _canMove;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _controller = GetComponent<CharacterController>();
        _canMove = true;
    }

    public void BlockMove()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            return;
        }

        var xInput = _joystick.Horizontal;
        var yInput = _joystick.Vertical;

        _controller.Move((Vector3.right * xInput + Vector3.forward * yInput) * _speed * Time.deltaTime);
        if (Mathf.Abs(xInput) > 0.1f || Mathf.Abs(yInput) > 0.1f)
        {
            _animator.SetFloat("Speed", 10f);
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }
        transform.LookAt(transform.position + (Vector3.right * xInput + Vector3.forward * yInput) * _speed * Time.deltaTime);

        var isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }

        _gravityVelocity += Vector3.up * _gravityMultyplier * Time.deltaTime;
        _controller.Move(_gravityVelocity);
    }
}
