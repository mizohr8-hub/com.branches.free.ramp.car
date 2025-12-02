using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTire;
    [SerializeField] private Rigidbody2D _backTire;
    [SerializeField] private Rigidbody2D _carRigidBody;
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _rotationSpeed = 200f;

    private float _moveInput;

    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate()
    {
       
        Debug.Log("baka, value to torque is " + -_moveInput + _speed + Time.fixedDeltaTime);
        _frontTire.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _backTire.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _carRigidBody.AddTorque(-_moveInput * _rotationSpeed * Time.fixedDeltaTime);

    }
}
