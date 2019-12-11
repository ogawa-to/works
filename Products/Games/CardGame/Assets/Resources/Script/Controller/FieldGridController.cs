using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// フィールドの各マス目の操作
public class FieldGridController : MonoBehaviour
{
    public const int GRID_X_MIN = 0;
    public const int GRID_X_MAX = 3;
    public const int GRID_Y_MIN = 0;
    public const int GRID_Y_MAX = 3;

    // フィールド上のマス座標
    public Vector2Int gridPosition { get; set; }

    public void Init()
    {
        Draw();
    }

    // TODOテスト用配色
    public void Draw()
    {

        // マスに色を付ける。
        CardController card = GetComponentInChildren<CardController>();
        Image image = GetComponent<Image>();
        if (card == null)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        } else if (card.model.gameSide == GameSide.Player)
        {
            image.color = new Color(0.0f, 0.0f, 0.5f, 0.3f);
        } else if (card.model.gameSide == GameSide.Enemy)
        {
            image.color = new Color(0.5f, 0.0f, 0.0f, 0.3f);
        }
    }
}
