using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ゲーム進行を確認するためのデバッグ用ログクラス
public class Log : MonoBehaviour
{
    FPSUtils fpsUtils;
    string mMessage;


    void Start()
    {
        fpsUtils = GameObject.Find("FPS").GetComponent<FPSUtils>();
    }

    void Update()
    {
        mMessage = "";
        AddMessage("FPS : " + fpsUtils.mFps.ToString("F2"));
        this.GetComponent<Text>().text = mMessage;

    }

    // 表示するメッセージを追加する。
    public void AddMessage(string message)
    {
        mMessage += message;
        mMessage += "\r\n";
    }
}
