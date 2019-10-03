using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　ホールド中のミノを監視するディレクター
public class HoldDirector : MonoBehaviour
{

    private List<GameObject> holdMino;

    // ホールド済みであるかどうかのフラグ
    private bool alreadHoldFlag;

    void Start()
    {
        alreadHoldFlag = false;
        holdMino = new List<GameObject>();
    }

    void Update()
    {

    }

    public bool HasHold()
    {
        return alreadHoldFlag;
    }

    // ホールドする。
    public void DoHold(List<GameObject> mino)
    {
        // ホールド済みのオブジェクトを削除する。
        foreach (GameObject block in holdMino)
        {
            Destroy(block);
        }
        holdMino.Clear();

        // 新しいミノをホールドする。
        foreach (GameObject block in mino)
        {
            int offsetBlockX = block.GetComponent<MinoController>().offsetBlockX;
            int offsetBlockY = block.GetComponent<MinoController>().offsetBlockY;
            block.transform.position = new Vector3(-3.5f + offsetBlockX * 0.35f, -0.5f - offsetBlockY * 0.35f, 0);
            holdMino.Add(block);
        }
        alreadHoldFlag = true;
    }

    // ホールドしているミノの種類を取得する。
    public int GetMinoKind()
    {
        return holdMino[0].GetComponent<MinoController>().minoKind;
    }
}
