//*********************************************
// 概　要：汎用シングルトン
// 作成者：ta.kusumoto
// 作成日：2022/08/04

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
        // 他のゲームオブジェクトにアタッチされているか確認
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

        // アタッチされている場合は破棄
        Destroy(this);
        Debug.Log("オブジェクトを破棄しました");
        return false;
    }
}
