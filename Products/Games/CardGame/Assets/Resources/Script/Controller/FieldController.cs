using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// フィールドの操作
public class FieldController : MonoBehaviour
{
    // シングルトン
    public static FieldController instance;
    
    // 四方の座標を表すマップ
    public static readonly Dictionary<Direction, Vector2Int> AllAroundDirection = new Dictionary<Direction, Vector2Int>
    {
        [Direction.UP] = new Vector2Int(0, -1)
        , [Direction.DOWN] = new Vector2Int(0, 1)
        , [Direction.LEFT] = new Vector2Int(-1, 0)
        , [Direction.RIGHT] = new Vector2Int(1, 0)
    };

    // シングルトンのインスタンスを保持する。
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // カード同士で戦闘する。
    public void CardBattle(CardController attacker)
    {
        // 四方に対して、カードが存在した場合に双方で攻撃を行う。
        foreach(var pair in AllAroundDirection)
        {
            CardController defender = GetArroundCard(attacker, pair.Value);
            // カード自体が存在しない場合
            if (defender == null)
            {
                continue;
            }
            // 敵でない場合
            if (attacker.model.gameSide == defender.model.gameSide)
            {
                continue;
            }

            attacker.Attack(defender, pair.Key);
            defender.Attack(attacker, DirectionUtil.getReverse(pair.Key));

            // attacker.RefreshView();
            // defender.RefreshView();

            // 死んでいたら、底にカードを置けるようにする。
            // カードは死ぬ。
            // 再度そこにカードを置けるようにする。
            if (attacker.IsDeath())
            {
                FieldGridController controller = attacker.GetComponentInParent<FieldGridController>();
                controller.GetComponent<DropCardToFieldEvent>().enabled = true;
                attacker.Death();

                // 死亡回数をカウントする。
                GameManager.instance.IncrementDeathCount(GameSide.Player);
            }
            if (defender.IsDeath())
            {
                FieldGridController controller = defender.GetComponentInParent<FieldGridController>();
                controller.GetComponent<DropCardToFieldEvent>().enabled = true;
                defender.Death();
                GameManager.instance.IncrementDeathCount(GameSide.Enemy);

            }
        }
    }

    // 指定した位置のカードを取得する。
    public CardController GetArroundCard(CardController origin, Vector2Int offset)
    {
        // フィールドの全てのカードを取得する。
        CardController[] cards = GetComponentsInChildren<CardController>();

        // ターゲットのマス座標
        Vector2Int target = origin.model.gridPosition + offset;

        // マス座標の一致したカードを返却する。
        CardController card = Array.Find(cards, c => (c.model.gridPosition == target));
        return card;
    }

    // 味方または敵の全てのカードを取得する。
    public CardController[] GetAllCards(GameSide gameSide)
    {
        CardController[] cards = GetComponentsInChildren<CardController>();
        cards = Array.FindAll(cards, card => card.model.gameSide == gameSide);
        return cards;
    }

    // 味方または敵の全てのカードの枚数を取得する。
    public int GetAllCardsCount(GameSide gameSide)
    {
        return GetAllCards(gameSide).Length;
    }

    // 全ての空き場所を取得する。
    public FieldGridController[] GetEmptyGrid()
    {
        FieldGridController[] allGrid = GetComponentsInChildren<FieldGridController>();
        FieldGridController[] emptyGrids = Array.FindAll(allGrid, 
            grid => grid.GetComponentInChildren<CardController>() == null);

        return emptyGrids;
    }
}
