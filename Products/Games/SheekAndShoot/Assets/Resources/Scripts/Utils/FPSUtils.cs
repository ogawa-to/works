using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class FPSUtils : MonoBehaviour
{
    float mDeltaTime;
    int mFrameCount;
    public float mFps { get; private set; }

    public void Start()
    {
        mDeltaTime = 0.0f;
        mFrameCount = 0;
    }
    public void Update()
    {
        mDeltaTime += Time.deltaTime;
        mFrameCount++;
        // 1秒経過
        if (mDeltaTime >= 1.0f)
        {
            mFps = mFrameCount / mDeltaTime;
        }
    }
}