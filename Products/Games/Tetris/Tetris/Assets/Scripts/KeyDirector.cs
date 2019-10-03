using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDirector : MonoBehaviour
{
    // キーコードからキーボードに割り当てられたコードを網羅されるよう適当な値を設定。
    private const int KEY_MAX = 500;

    int[] beginPushTime = new int[KEY_MAX];

    void Start()
    {
        for (int i = 0; i < KEY_MAX; i++)
        {
            beginPushTime[i] = 0;
        }
    }

    void Update()
    {
        for (int i = 0; i < KEY_MAX; i++)
        {
            if (Input.GetKey((KeyCode)i))
            {
                if (beginPushTime[i] == 0)
                {
                    beginPushTime[i] = Time.frameCount;
                }
            }
            else
            {
                beginPushTime[i] = 0;
            }
        }
    }

    // キーの押下時間(フレーム単位)を取得する。
    public int GetKeyPushTime(KeyCode keyCode)
    {
        // 現在のフレーム時間と押下開始時間の差を取る。
        int i = (int)keyCode;
        if (beginPushTime[(int)keyCode] == 0)
        {
            return 0;
        }
        else
        {
            return Time.frameCount - beginPushTime[(int)keyCode];
        }
    }
}
