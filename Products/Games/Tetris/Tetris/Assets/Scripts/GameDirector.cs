using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    // 操作中のミノ
    private List<GameObject> mino;

    private GameObject minoPrefub;

    private GameObject[,] field;

    private HoldDirector holdDirector;

    private NextMinoDirector nextMinoDirector;
    private KeyDirector keyDirector;

    private const int MAX_BLOCK_X = 10;
    private const int MAX_BLOCK_Y = 20;

    // UI群
    private Text levelText;
    private Text scoreText;
    private Text linesText;
    private int level;
    private int score;
    private int lines;

    // ホールドが可能かどうか。
    bool canHold;
    // 自由落下するまでの時間
    int fallCount;

    void Start()
    {
        // プレハブオブジェクトを取得する。
        this.minoPrefub = GameObject.Find("minoPrefub") as GameObject;

        field = new GameObject[MAX_BLOCK_X, MAX_BLOCK_Y];
        holdDirector = GameObject.Find("HoldDirector").GetComponent<HoldDirector>();

        // ミノを作成する。
        nextMinoDirector = GameObject.Find("NextMinoDirector").GetComponent<NextMinoDirector>();
        nextMinoDirector.InitializeMino();
        this.mino = nextMinoDirector.GetNextMino();

        // キー監視オブジェクトを取得する。
        keyDirector = GameObject.Find("KeyDirector").GetComponent<KeyDirector>();

        // UIを初期化する。
        levelText = GameObject.Find("Level").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        linesText = GameObject.Find("Lines").GetComponent<Text>();
        level = 1;
        score = 0;
        lines = 0;

        canHold = true;
    }

    void Update()
    {
        this.UserInput();
        this.Fall();
        Debug.Log(this.fallCount);
        this.Draw();
    }

    // ユーザの操作を受け付ける。
    private void UserInput()
    {
        int pushTime;
        // 左キー
        pushTime = this.keyDirector.GetKeyPushTime(KeyCode.LeftArrow);
        // 3フレーム単位で受け付ける。
        if (pushTime % 3 == 1)
        {
            bool isMovable = true;
            foreach (GameObject block in mino)
            {
                // 移動後の座標
                int blockX = block.GetComponent<MinoController>().blockX - 1;
                int blockY = block.GetComponent<MinoController>().blockY;

                if (blockX < 0 || field[blockX, blockY] != null)
                {
                    isMovable = false;
                    break;
                }
            }
            // 移動可能であれば左へ1マス移動する。
            if (isMovable)
            {
                this.Move(-1, 0);
            }
        }

        // 右キー
        pushTime = this.keyDirector.GetKeyPushTime(KeyCode.RightArrow);
        if (pushTime % 3 == 1)
        {
            bool isMovable = true;
            foreach (GameObject block in mino)
            {
                int blockX = block.GetComponent<MinoController>().blockX + 1;
                int blockY = block.GetComponent<MinoController>().blockY;

                if (blockX > MAX_BLOCK_X - 1 || field[blockX, blockY] != null) 
                {
                    isMovable = false;
                    break;
                }
            }
            if (isMovable)
            {
                this.Move(1, 0);
            }
        }

        // 下キー
        pushTime = this.keyDirector.GetKeyPushTime(KeyCode.DownArrow);
        if (pushTime % 3 == 1)
        {
            // 接地判定
            bool isMovable = true;
            foreach (GameObject block in mino)
            {
                int blockX = block.GetComponent<MinoController>().blockX;
                int blockY = block.GetComponent<MinoController>().blockY + 1;

                // 接地した場合
                if (blockY > MAX_BLOCK_Y - 1 || field[blockX, blockY] != null)
                {
                    isMovable = false;
                    this.Landing();
                    break;
                }
            }

            if (isMovable)
            {
                this.Move(0, 1);
            }

            // 落下までの時間を設定する。
            this.SetFallCount();
            this.score += 10;
        }

        // 上キー (ハードドロップ)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 接地判定
            bool isMovable = true;
            int moveValue = 0;
            while (isMovable)
            {
                foreach (GameObject block in mino)
                {
                    int blockX = block.GetComponent<MinoController>().blockX;
                    int blockY = block.GetComponent<MinoController>().blockY + 1 + moveValue;

                    // 接地した場合
                    if (blockY > MAX_BLOCK_Y - 1 || field[blockX, blockY] != null)
                    {
                        isMovable = false;
                        break;
                    }
                }
                moveValue++;
            }

            this.Move(0, moveValue - 1);
            this.Landing();

            // 落下までの時間を設定する。
            this.SetFallCount();
            this.score += 100;
        }

        // Zキー (ホールド)
        if (Input.GetKeyDown(KeyCode.Z)) {

            // ホールドができる状態であれば
            if (canHold)
            {
                List<GameObject> newMino;

                // 初めてホールドする場合
                if (!holdDirector.HasHold())
                {
                    // ネクストを次のミノとする。
                    // 新しいミノを生成する。
                    newMino = this.nextMinoDirector.GetNextMino();
                }
                else
                {
                    int holdMinoKind = holdDirector.GetMinoKind();
                    // ホールド中のミノを次のミノとする。
                    newMino = minoPrefub.GetComponent<MinoPrefub>().CreateMino(holdMinoKind, true);
                }

                // ミノをホールドする。
                holdDirector.DoHold(mino);

                // 新しいミノを設定する。
                mino = newMino;

                // ホールドを不可とする。
                this.canHold = false;
            }

            // 落下までの時間を設定する。
            this.SetFallCount();
        }

        // Xキー (左回転)
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Oミノのみ回転させない。
            if (mino[0].GetComponent<MinoController>().minoKind != 1)
            {
                bool isMovable = true;
                List<Vector2Int> rotatedPos = new List<Vector2Int>();
                foreach (GameObject block in mino)
                {
                    MinoController mc = block.GetComponent<MinoController>();
                    // 回転軸の座標
                    int axisX = mc.blockX - mc.offsetBlockX;
                    int axisY = mc.blockY - mc.offsetBlockY;

                    // 回転後のoffet座標 (X <- -Y, Y <- X)
                    int rotatedX = axisX - mc.offsetBlockY;
                    int rotatedY = axisY + mc.offsetBlockX;

                    // 衝突判定
                    if (rotatedX < 0 || rotatedX > MAX_BLOCK_X - 1 || rotatedY < 0 || rotatedY > MAX_BLOCK_Y - 1)
                    {
                        isMovable = false;
                        break;
                    }
                    if (field[rotatedX, rotatedY] != null)
                    {
                        isMovable = false;
                        break;
                    }
                }

                // 衝突チェック
                if (isMovable)
                {
                    foreach (GameObject block in mino)
                    {
                        MinoController mc = block.GetComponent<MinoController>();

                        // 回転軸の座標
                        int axisX = mc.blockX - mc.offsetBlockX;
                        int axisY = mc.blockY - mc.offsetBlockY;
                        int rotatedOffsetX = -mc.offsetBlockY;
                        int rotatedOffsetY = +mc.offsetBlockX;

                        // 回転後の座標
                        mc.blockX = axisX - mc.offsetBlockY;
                        mc.blockY = axisY + mc.offsetBlockX;

                        // 回転後のoffet座標 (X <- -Y, Y <- X)
                        mc.offsetBlockX = rotatedOffsetX;
                        mc.offsetBlockY = rotatedOffsetY;

                    }
                }
            }
        }
        // Cキー (右回転)
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // Oミノのみ回転させない。
            if (mino[0].GetComponent<MinoController>().minoKind != 1)
            {


                bool isMovable = true;
                List<Vector2Int> rotatedPos = new List<Vector2Int>();
                foreach (GameObject block in mino)
                {
                    MinoController mc = block.GetComponent<MinoController>();
                    // 回転軸の座標
                    int axisX = mc.blockX - mc.offsetBlockX;
                    int axisY = mc.blockY - mc.offsetBlockY;

                    // 回転後のoffet座標 (X <- -Y, Y <- X)
                    int rotatedX = axisX + mc.offsetBlockY;
                    int rotatedY = axisY - mc.offsetBlockX;

                    // 衝突判定
                    if (rotatedX < 0 || rotatedX > MAX_BLOCK_X - 1 || rotatedY < 0 || rotatedY > MAX_BLOCK_Y - 1)
                    {
                        isMovable = false;
                        break;
                    }
                    if (field[rotatedX, rotatedY] != null)
                    {
                        isMovable = false;
                        break;
                    }
                }

                // 衝突チェック
                if (isMovable)
                {
                    foreach (GameObject block in mino)
                    {
                        MinoController mc = block.GetComponent<MinoController>();

                        // 回転軸の座標
                        int axisX = mc.blockX - mc.offsetBlockX;
                        int axisY = mc.blockY - mc.offsetBlockY;
                        int rotatedOffsetX = mc.offsetBlockY;
                        int rotatedOffsetY = -mc.offsetBlockX;

                        // 回転後の座標
                        mc.blockX = axisX + mc.offsetBlockY;
                        mc.blockY = axisY - mc.offsetBlockX;

                        // 回転後のoffet座標 (X <- -Y, Y <- X)
                        mc.offsetBlockX = rotatedOffsetX;
                        mc.offsetBlockY = rotatedOffsetY;

                    }
                }
            }
        }
    }

    private void Fall()
    {
        this.fallCount--;
        // 指定時間になった時、テトリミノを落下させる。
        if (this.fallCount < 0)
        {
            // 落下までの時間を設定する。
            this.SetFallCount();

            // 接地判定
            bool isMovable = true;
            foreach (GameObject block in mino)
            {
                int blockX = block.GetComponent<MinoController>().blockX;
                int blockY = block.GetComponent<MinoController>().blockY + 1;

                // 接地した場合
                if (blockY > MAX_BLOCK_Y - 1 || field[blockX, blockY] != null)
                {
                    isMovable = false;
                    this.Landing();
                    break;
                }
            }

            if (isMovable)
            {
                this.Move(0, 1);
            }
        }
    }

    private void SetFallCount()
    {
        this.fallCount = 120 - this.level * 5;
    }

    // ミノを移動させる。
    private void Move(int offsetX, int offsetY)
    {
        foreach (GameObject block in mino)
        {
            block.GetComponent<MinoController>().blockX += offsetX;
            block.GetComponent<MinoController>().blockY += offsetY;
        }
    }

    // ミノが地面に着した際の処理。
    private void Landing() 
    {
        foreach (GameObject block in mino)
        {
            int blockX = block.GetComponent<MinoController>().blockX;
            int blockY = block.GetComponent<MinoController>().blockY;
            field[blockX, blockY] = block;
        }

        // ライン削除判定処理を行う。
        this.DeleteLineJudge();

        // 新しいミノを生成する。
        this.mino = this.nextMinoDirector.GetNextMino();

        // ホールドを可能にする。
        this.canHold = true;
    }


    // ラインがそろっていたら削除する。
    private void DeleteLineJudge()
    {
        int moveValue = 0;
        for (int y = MAX_BLOCK_Y - 1; y >= 0; y--)
        {
            // 削除対象かチェックする。
            bool deletedFlag = true;
            for (int x = 0; x < MAX_BLOCK_X; x++)
            {
                if (field[x, y] == null)
                {
                    deletedFlag = false;
                }
            }

            //削除対象であればライン単位で削除する。
            if (deletedFlag)
            {
                for (int x = 0; x < MAX_BLOCK_X; x++)
                {
                    Destroy(field[x, y]);
                }
                moveValue++;
            // そうでない場合、ミノを下にずらす。
            } else
            {
                if (moveValue > 0)
                {
                    for (int x = 0; x < MAX_BLOCK_X; x++)
                    {
                        field[x, y + moveValue] = field[x, y];
                        if (field[x, y + moveValue] != null)
                        {
                            field[x, y + moveValue].GetComponent<MinoController>().blockY += moveValue;
                        }
                    }
                }
            }
        }

        this.lines += moveValue;
        if (1 <= moveValue && moveValue <= 3)
        {
            this.score += 1000 * moveValue;

            // 効果音処理を暫定ここに書く (外だしクラスとしたい。)
            AudioSource audioSource;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Sounds/1_3line") as AudioClip;
            audioSource.Play();

        }
        // Back to Back
        else if (moveValue == 4)
        {
            this.score += 10000;
        }
        this.level = this.score / 1000;
    }

    // 描画を行う。
    private void Draw()
    {
        // ミノの描画
        foreach (GameObject block in mino)
        {
            int blockX = block.GetComponent<MinoController>().blockX;
            int blockY = block.GetComponent<MinoController>().blockY;
            block.transform.position = new Vector3(-1.75f + blockX * 0.35f, 3.5f - blockY * 0.35f, 0);
        }
        for (int i = 0; i < MAX_BLOCK_X; i++)
        {
            for (int j = 0; j < MAX_BLOCK_Y; j++)
            {
                if (field[i, j] != null)
                {
                    int blockX = field[i, j].GetComponent<MinoController>().blockX;
                    int blockY = field[i, j].GetComponent<MinoController>().blockY;
                    field[i, j].transform.position = new Vector3(-1.75f + blockX * 0.35f, 3.5f - blockY * 0.35f, 0);
                }
            }
        }

        // UIを描画する。
        levelText.text = level.ToString();
        scoreText.text = score.ToString("D8");
        linesText.text = lines.ToString();


    }

}
