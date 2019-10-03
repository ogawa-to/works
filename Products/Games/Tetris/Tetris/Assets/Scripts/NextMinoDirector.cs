using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMinoDirector : MonoBehaviour
{
    // テトリミノの種類の一覧
    Queue<int> minoKindQueue;

    // プレハブオブジェクト
    MinoPrefub minoPrefub;

    // 表示用ミノの数
    private const int DISPLAY_MINO_NUM = 4;

    // 表示用のネクストミノ
    List<GameObject>[] displayNextMino;

    void Start()
    {
        // プレハブオブジェクトを取得する。
        minoPrefub = GameObject.Find("minoPrefub").GetComponent<MinoPrefub>();

        // 初期処理として2サイクル分のNextを用意しておく。
        minoKindQueue = new Queue<int>();
        for (int i = 0; i < 2; i++)
        {
            this.CreateMino();
        }
    }

    void Update()
    {
        this.Draw();
    }

    // 次のミノを取得する。
    public List<GameObject> GetNextMino()
    {
        List<GameObject> nextMino;
        nextMino = displayNextMino[0];

        for (int i = 0; i < DISPLAY_MINO_NUM - 1; i++)
        {
            displayNextMino[i] = displayNextMino[i + 1];
        }
        int minoKind = minoKindQueue.Dequeue();
        displayNextMino[DISPLAY_MINO_NUM - 1] = minoPrefub.CreateMino(minoKind, true);

        // 1サイクル使い終わったタイミングで新たなミノを用意する。
        if (minoKindQueue.Count == 7)
        {
            this.CreateMino();  
        }

        return nextMino;
    }

    // ミノを生成する。
    private void CreateMino()
    {
        List<int> minoKindList = RandomUtils.GetNoOverlapList(0, 7, 7);
        foreach (int k in minoKindList)
        {
            minoKindQueue.Enqueue(k);
        }
    }

    // 描画処理を行う。
    private void Draw()
    {
        for (int i = 0; i < DISPLAY_MINO_NUM; i++)
        {
            foreach (GameObject block in displayNextMino[i])
            {
                int offsetBlockX = block.GetComponent<MinoController>().offsetBlockX;
                int offsetBlockY = block.GetComponent<MinoController>().offsetBlockY;
                float displayX = 3.0f + offsetBlockX * 0.35f;
                float displayY = 3.0f - (offsetBlockY * 0.35f) - (i * 1.5f);
                block.transform.position = new Vector3(displayX, displayY, 0);
            }
        }
    }

    public void InitializeMino()
    {
        // 初期処理として4つのミノを生成する。
        displayNextMino = new List<GameObject>[DISPLAY_MINO_NUM];
        for (int i = 0; i < DISPLAY_MINO_NUM; i++)
        {
            int minoKind = minoKindQueue.Dequeue();
            displayNextMino[i] = minoPrefub.CreateMino(minoKind, true);
        }
    }

    
}
