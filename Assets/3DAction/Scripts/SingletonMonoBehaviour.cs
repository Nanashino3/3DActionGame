//*********************************************
// �T�@�v�F�ėp�V���O���g��
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null) {
                _instance = (T)FindObjectOfType(typeof(T));
                if(_instance == null) { Debug.LogError(typeof(T) + "is nothing"); }
            }

            return _instance;
        }
    }

    virtual protected void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩�m�F
        if (CheckInstance()) {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected bool CheckInstance()
    {
        if(_instance == null) {
            _instance = this as T;
            return true;
        }else if (Instance == this) {
            return true;
        }

        // �A�^�b�`����Ă���ꍇ�͔j��
        Destroy(this);
        Debug.Log("�I�u�W�F�N�g��j�����܂���");
        return false;
    }
}
