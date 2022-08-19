using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    static readonly int _hashAttack = Animator.StringToHash("Attack");
    static readonly int _hashAttackType = Animator.StringToHash("AttackType");

    public enum E_STATUS
    {
        Normal,
        Move,
        Attack,

        MaxStatus
    };

    private Animator _animator;
    private E_STATUS _status;

    public bool IsMovable => E_STATUS.Normal == _status;    // à⁄ìÆâ¬î\Ç©
    public bool IsAttackable => E_STATUS.Normal == _status; // çUåÇâ¬î\Ç©

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void GoToAttackIfPossible(int attackType)
    {
        _status = E_STATUS.Attack;
        _animator.SetTrigger(_hashAttack);
        _animator.SetInteger(_hashAttackType, attackType);
    }

    public void GoToNormalIfPossible()
    {
        _status = E_STATUS.Normal;
    }
}
