using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 落下するミノを生成するプレハブ
public class MinoPrefub : MonoBehaviour
{
    // プレハブオブジェクト
    public GameObject minoPrefub;

    public enum MinoKind{
        I = 0,
        O = 1,
        S = 2,
        Z = 3,
        J = 4,
        L = 5,
        T = 6
    };

    private const int MINO_KIND_NUM = 7;
    private Vector2Int[][] blockInfo;

    // スプライト
    private Sprite[] sprites;

    void Start()
    {

        // テトリミノの座標を初期化する。
        blockInfo = new Vector2Int[MINO_KIND_NUM][];

        // Iミノ
        blockInfo[0] = new[] {
            new Vector2Int(-2, 0)
            , new Vector2Int(-1, 0)
            , new Vector2Int(0, 0)
            , new Vector2Int(1, 0)
        };

        // Oミノ
        blockInfo[1] = new[] {
            new Vector2Int(0, 0)
            , new Vector2Int(0, -1)
            , new Vector2Int(1, 0)
            , new Vector2Int(1, -1)
        };

        // Sミノ
        blockInfo[2] = new[] {
            new Vector2Int(0, 0)
            , new Vector2Int(1, 0)
            , new Vector2Int(0, -1)
            , new Vector2Int(-1, -1)
        };

        
        // Zミノ
        blockInfo[3] = new[] {
            new Vector2Int(0, 0)
            , new Vector2Int(-1, 0)
            , new Vector2Int(0, -1)
            , new Vector2Int(1, -1)
        };

        // Jミノ
        blockInfo[4] = new[] {
            new Vector2Int(-1, 1)
            , new Vector2Int(0, 1)
            , new Vector2Int(1, 1)
            , new Vector2Int(1, 0)
        };

        // Lミノ
        blockInfo[5] = new[] {
            new Vector2Int(-1, 1)
            , new Vector2Int(0, 1)
            , new Vector2Int(1, 1)
            , new Vector2Int(-1, 0)
        };

        // Tミノ
        blockInfo[6] = new[] {
            new Vector2Int(0, 0)
            , new Vector2Int(-1, 0)
            , new Vector2Int(0, 1)
            , new Vector2Int(1, 0)
        };

        // テトリミノのスプライトをロードする。
        sprites = new Sprite[MINO_KIND_NUM];
        sprites[0] = Resources.Load<Sprite>("Images/mino_I");
        sprites[1] = Resources.Load<Sprite>("Images/mino_O");
        sprites[2] = Resources.Load<Sprite>("Images/mino_S");
        sprites[3] = Resources.Load<Sprite>("Images/mino_Z");
        sprites[4] = Resources.Load<Sprite>("Images/mino_J");
        sprites[5] = Resources.Load<Sprite>("Images/mino_L");
        sprites[6] = Resources.Load<Sprite>("Images/mino_T");
    }

    void Update()
    {
    }

    // テトリミノを生成する。
    public List<GameObject> CreateMino(int pieceKind, bool isMove)
    {

        // テトリミノ
        List<GameObject> minoList = new List<GameObject>();

        // ピース数分生成する。
        for (int i = 0; i < blockInfo[pieceKind].Length; i++)
        {
            // テトリミノを構成するブロックピースを生成する。
            GameObject blockPiece = Instantiate(minoPrefub) as GameObject;

            int offsetBlockX = blockInfo[pieceKind][i].x;
            int offsetBlockY = blockInfo[pieceKind][i].y;
            blockPiece.GetComponent<MinoController>().offsetBlockX = offsetBlockX;
            blockPiece.GetComponent<MinoController>().offsetBlockY = offsetBlockY;

            // 操作するミノに対する初期化
            if (isMove) {

                const int INITIAL_BLOCK_X = 5;
                const int INITIAL_BLOCK_Y = 1;
                int blockX = offsetBlockX + INITIAL_BLOCK_X;
                int blockY = offsetBlockY + INITIAL_BLOCK_Y;
                // ブロック座標を設定する。
                blockPiece.GetComponent<MinoController>().blockX = offsetBlockX + INITIAL_BLOCK_X;
                blockPiece.GetComponent<MinoController>().blockY = offsetBlockY + INITIAL_BLOCK_Y;
            }

            // ネクスト，ホールドのミノに対する初期化
            else
            {
                blockPiece.GetComponent<MinoController>().blockX = -1;
                blockPiece.GetComponent<MinoController>().blockY = -1;
            }

            blockPiece.GetComponent<MinoController>().minoKind = pieceKind;
            // スプライトを変更する。
            blockPiece.GetComponent<SpriteRenderer>().sprite = sprites[pieceKind];
            
            minoList.Add(blockPiece);
        }

        return minoList;
    }
}
