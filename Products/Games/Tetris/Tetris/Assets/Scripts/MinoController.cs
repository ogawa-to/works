using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mino１つ分のオブジェクト
public class MinoController : MonoBehaviour
{
    // 升目上の座標。
    // NextやHoldに位置するミノについては-1を設定する。
    public int blockX { get; set; }
    public int blockY { get; set; }

    // 回転軸からの相対升目座標
    public int offsetBlockX { get; set; }
    public int offsetBlockY { get; set; }

    public int minoKind;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
