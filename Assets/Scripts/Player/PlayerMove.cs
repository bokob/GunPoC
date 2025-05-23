using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _moveInput;
    float _moveSpeed = 5f;
    [SerializeField] bool _isCanMove;

    //PlayerAttack _playerAttack;

    void Start()
    {
        //_playerAttack = GetComponent<PlayerAttack>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _moveInput = Managers.Input.MoveInput;
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }
}