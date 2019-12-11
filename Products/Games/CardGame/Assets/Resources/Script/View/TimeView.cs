using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 時間の外見
public class TimeView : MonoBehaviour
{
    [SerializeField] private Text timeText = default;
    // 描画する。
    public void Draw(TimeModel model)
    {
        float time = model.restTime;
        int minute = (int)time / 60;
        int second = (int)time % 60;
        timeText.text = minute.ToString() + ":" + second.ToString("D2");
    }
}
