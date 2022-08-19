using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
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

    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private float _rotateSpeed = 600;

    static readonly int _hashMove = Animator.StringToHash("MoveSpeed");
    static readonly int _hashJump = Animator.StringToHash("IsJump");
    static readonly int _hashFall = Animator.StringToHash("IsGround");

    // Unity�@�\
    private Vector3 _moveVelocity;
    private Rigidbody _rigidbody;
    private Quaternion _targetRotation;
    private Animator _animator;

    // ����X�N���v�g
    private PlayerAttack _attack;
    private PlayerStatus _status;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _attack = GetComponent<PlayerAttack>();
        _status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        Attack();
        Move();

        _animator.GetAnimatorTransitionInfo(0);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveVelocity * _moveSpeed + Vector3.up *_rigidbody.velocity.y;
    }

    //*************************************
    // �ړ�����
    private void Move()
    {
        if (_status.IsMovable) {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            // �J�����̌����ɍ��킹�đO���v�Z������
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            _moveVelocity = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;

            // ���x�̎擾
            var rotationSpeed = _rotateSpeed * Time.deltaTime;

            // �ړ�����������
            if (_moveVelocity.magnitude > 0.5f) {
                _targetRotation = Quaternion.LookRotation(_moveVelocity, Vector3.up);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed);

            // �W�����v
            bool isJump = false;
            if (IsGround() && Input.GetButtonDown("Jump")) {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.velocity = Vector3.up * _jumpPower;
                isJump = true;
            }
            // �A�j���[�V�����ݒ�
            _animator.SetFloat(_hashMove, _moveVelocity.magnitude * _moveSpeed);
            _animator.SetBool(_hashJump, isJump);
            _animator.SetBool(_hashFall, IsGround());

        } else {
            _moveVelocity = Vector3.zero;
        }
    }

    //*************************************
    // �U������
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1")) {
            _attack.Attack(0);
        } else if (Input.GetButtonDown("Fire2")) {
            _attack.Attack(1);
        }
    }

    //*************************************
    // �ڒn���菈��
    private bool IsGround()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + _groundCheckOffsetY * Vector3.up,
                                  _groundCheckRadius,
                                  Vector3.down,
                                  out hit,
                                  _groundCheckDistance,
                                  _groundCheckLayerMask,
                                  QueryTriggerInteraction.Ignore);
    }

    //*************************************
    // �ڒn����p�M�Y���`��
    private void OnDrawGizmos()
    {
        if (!_isGroundCheckGizmo) { return; }
        Gizmos.DrawSphere(transform.position + new Vector3(0, _groundCheckOffsetY, 0), _groundCheckRadius);
    }
}
