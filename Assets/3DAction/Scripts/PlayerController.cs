using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private Animator _animator;

    [Header("接地判定用：デバッグ用ギズモ(ON/OFF)")]
    [SerializeField] private bool _isGroundCheckGizmo;
    [Header("接地判定用：判定するレイヤーマスク")]
    [SerializeField] private LayerMask _groundCheckLayerMask;
    [Header("接地判定用：半径")]
    [SerializeField] private float _groundCheckRadius;
    [Header("接地判定用：オフセットY")]
    [SerializeField] private float _groundCheckOffsetY;
    [Header("接地判定用：判定対象までの距離")]
    [SerializeField] private float _groundCheckDistance;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private RaycastHit _hit;

    private Vector3 _moveVelocity;
    private Quaternion _targetRotation;

    private void Start()
    {
        _transform = transform;
        _targetRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var axisX = Input.GetAxis("Horizontal");
        var axisZ = Input.GetAxis("Vertical");
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        _moveVelocity = horizontalRotation * new Vector3(axisX, 0, axisZ).normalized;
        var rotationSpeed = 600 * Time.deltaTime;

        if(_moveVelocity.magnitude > 0.5f) {
            _targetRotation = Quaternion.LookRotation(_moveVelocity, Vector3.up);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed);

        _animator.SetFloat("MoveSpeed", _moveVelocity.magnitude * _moveSpeed, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _moveVelocity.y = 0;
        _rigidbody.velocity = _moveVelocity * _moveSpeed + Vector3.up * _rigidbody.velocity.y;
    }

    // 接地判定
    private bool IsGrounded()
    {
        return Physics.SphereCast(_transform.position + _groundCheckOffsetY * Vector3.up,
                                  _groundCheckRadius,
                                  Vector3.down,
                                  out _hit,
                                  _groundCheckDistance,
                                  _groundCheckLayerMask,
                                  QueryTriggerInteraction.Ignore);
    }

    // 接地判定用ギズモ描画
    private void OnDrawGizmos()
    {
        if (!_isGroundCheckGizmo) { return; }
        Gizmos.DrawSphere(transform.position + new Vector3(0, _groundCheckOffsetY, 0), _groundCheckRadius);
    }
}
