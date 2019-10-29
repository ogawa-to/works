using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キーの入力情報についてのクラス
public class KeyUtil : MonoBehaviour
{
    // UnityEngine.KeyCodeに割り振られた連番数
    const int KEY_MAX = 509;

    // 押下時間 (フレーム)
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
            int time = Time.frameCount - beginPushTime[(int)keyCode] + 1;
            return time;
        }
    }

    // 定期時間ごとに押下されているかどうか。
    // ex. intervalTime=5の場合，1, 6, 11...フレームでtrueとなる。
    public bool IsMatchKeyInterval(KeyCode keyCode, int intervalTime)
    {
        int i = (int)keyCode;
        if (beginPushTime[(int)keyCode] == 0)
        {
            return false;
        }
        else
        {
            int time = Time.frameCount - beginPushTime[(int)keyCode];
            if (time % intervalTime == 0) {
                return true;
            }
            return false;
        }
    }
}
