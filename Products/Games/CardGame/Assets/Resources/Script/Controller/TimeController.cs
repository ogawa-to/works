using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間の操作
public class TimeController : MonoBehaviour
{
    private TimeModel model;
    private TimeView view;

    private void Awake()
    {
        model = new TimeModel();
        view = GetComponent<TimeView>();
    }

    private void Update()
    {
        model.ReduceTime();
        view.Draw(model);
    }

    public void Init(float restTime)
    {
        model.Init(restTime);
    }
}
