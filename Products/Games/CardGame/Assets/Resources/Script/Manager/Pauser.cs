using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Pauser : MonoBehaviour
{
    // 停止対象となる全てのオブジェクト
    private static List<Pauser> pauseObjectList = new List<Pauser>();
    public static bool isPause { get; private set; } = false;

    // 停止対象のオブジェクトが持つアクティブなコンポーネント
    private Behaviour[] activeBehaviours;


    private void Start()
    {
        // 停止対象に自身を登録する。
        pauseObjectList.Add(this);
    }

    // オブジェクトを停止する。
    private void OnPause()
    {
        // 全てのアクティブなコンポーネント停止する。(子は含めない。)
        Behaviour[] behaviours = GetComponentsInChildren<Behaviour>();
        activeBehaviours = Array.FindAll(behaviours, 
            b => (b != null && b.enabled && IsTargetType(b)));
        foreach (Behaviour c in activeBehaviours)
        {
            c.enabled = false;
        }
    }

    // 停止対象外であるかを判定する。
    private bool IsTargetType(Behaviour b)
    {
        // 描画に関するBehaviourは対象外にする。
        // (enable=falseで非表示になるため。)
        if (b is Image
            || b is Text) {
            return false;
        }
        return true;
    }

    // オブジェクトを再開する。
    private void OnResume()
    {
        foreach(Behaviour b in activeBehaviours)
        {
            b.enabled = true;
        }
        activeBehaviours = null;
    }

    // オブジェクトを停止対象から外す。
    private void OnDestroy()
    {
        pauseObjectList.Remove(this);
    }

    // 全てのオブジェクトを停止する。
    public static void Pause()
    {
        isPause = true;
        foreach (Pauser p in pauseObjectList)
        {
            p.OnPause();
        }
    }

    // 全てのオブジェクトを再開する。
    public static void Resume()
    {
        isPause = false;
        foreach(Pauser p in pauseObjectList)
        {
            p.OnResume();
        }
    }
}
