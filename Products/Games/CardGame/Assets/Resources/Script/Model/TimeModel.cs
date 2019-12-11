using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間の情報を保持する。
public class TimeModel
{
    // 残り時間
    public float restTime { get; set; }

    public void Init(float restTime)
    {
        this.restTime = restTime;
    }

    public void ReduceTime()
    {
        restTime -= Time.deltaTime;
    }
}
