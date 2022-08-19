using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStatus _status;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();

    }

    public void Attack(int attackType)
    {
        _status.GoToAttackIfPossible(attackType);
    }
}
