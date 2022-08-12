using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private Animator _animator;

    [Header("�ڒn����p�F�f�o�b�O�p�M�Y��(ON/OFF)")]
    [SerializeField] private bool _isGroundCheckGizmo;
    [Header("�ڒn����p�F���肷�郌�C���[�}�X�N")]
    [SerializeField] private LayerMask _groundCheckLayerMask;
    [Header("�ڒn����p�F���a")]
    [SerializeField] private float _groundCheckRadius;
    [Header("�ڒn����p�F�I�t�Z�b�gY")]
    [SerializeField] private float _groundCheckOffsetY;
    [Header("�ڒn����p�F����Ώۂ܂ł̋���")]
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

    // �ڒn����
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

    // �ڒn����p�M�Y���`��
    private void OnDrawGizmos()
    {
        if (!_isGroundCheckGizmo) { return; }
        Gizmos.DrawSphere(transform.position + new Vector3(0, _groundCheckOffsetY, 0), _groundCheckRadius);
    }
}
