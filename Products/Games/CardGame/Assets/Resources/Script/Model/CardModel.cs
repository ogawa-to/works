using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カードのデータ
public class CardModel
{
    public Sprite illust; // 画像
    public int[] attackPoints; // 四方の攻撃力
    public int hp; // HP
    public int cost; // コスト

    public int id; // ID
    public string name; // カード名
    public string text; // カードの説明文

    public List<Skill> skillList; // スキル

    // フィールド上のマス座標
    public Vector2Int gridPosition;

    public GameSide gameSide;
    public CardPlace cardPlace;

    // フィールドへ出すことができるか。
    public bool canMoveToField;
    // カードの待ち時間 (コストに対するマナの割合)
    public float waitAmount;

    // コンストラクタ
    public CardModel(int card_id, GameSide side, CardPlace place)
    {
        // 整数4桁の0埋め
        string str_id = card_id.ToString("D4");
        // カード情報を読み込む。
        CardEntity entity = Resources.Load<CardEntity>("CardEntityList/" + str_id);

        id = entity.id;
        name = entity.name;
        attackPoints = new int[(int)Direction.NUM];
        attackPoints[(int)Direction.UP] = entity.attackPointUp;
        attackPoints[(int)Direction.DOWN] = entity.attackPointDown;
        attackPoints[(int)Direction.LEFT] = entity.attackPointLeft;
        attackPoints[(int)Direction.RIGHT] = entity.attackPointRight;
        hp = entity.hp;
        cost = entity.cost;
        text = entity.text;
        illust = entity.illust;

        skillList = new List<Skill>();
        foreach (SkillEntity s in entity.skillEntityList)
        {
            skillList.Add(new Skill(s));
        }

        gameSide = side;

        canMoveToField = false;
        waitAmount = 0.0f;
    }

    // 攻撃をする。
    public void Attack (CardController card, Direction direction)
    {
        int attackPoint = attackPoints[(int)direction];
        card.model.Damage(attackPoint);
    }

    // HPを回復する。
    public void RecoverHp(int amount)
    {
        hp += amount;
    }
    // 攻撃力を増加する。
    public void IncreaseAttackPoint(Direction d, int amount)
    {
        attackPoints[(int)d] += amount;
    }
    // 攻撃力を減少する。
    public void DecreaseAttackPoint(Direction d, int amount)
    {
        attackPoints[(int)d] -= amount;
    }
    // ダメージを受ける。
    private void Damage(int amount)
    {
        hp -= amount;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    // 死亡チェック
    public bool CheckDeath ()
    {
        return hp == 0 ? true : false;
    }

    public void UseSkill(CardController user, Skill.Timing timing)
    {
        foreach (Skill skill in skillList)
        {
            // 発動タイミングがあえば発動する。
            if (skill.timing == timing)
            {
                skill.UseSkill(user);
            }
        }
    }

    // フィールドへ出せるかどうかのチェック
    public void CheckCanMoveToField()
    {
        if (cardPlace == CardPlace.HAND)
        {
            // コストが十分であれば、場に出せる状態にする。
            PlayerManaController mana = GameManager.instance.GetPlayerMana(gameSide);
            float manaAmount = mana.model.manaAmount;
            canMoveToField = (cost < manaAmount) ? true : false;
            waitAmount = manaAmount / (float)cost;
            waitAmount = (waitAmount > 1.0f) ? 1.0f : waitAmount;
            waitAmount = 1.0f - waitAmount;
        } else if (cardPlace == CardPlace.FIELD)
        {
            canMoveToField = false;
        }
    }
}