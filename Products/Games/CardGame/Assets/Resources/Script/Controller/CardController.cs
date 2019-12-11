using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カードの操作
public class CardController : MonoBehaviour
{
    // データ(model)に関する操作
    public CardModel model;

    // ビュー(view)に関する操作
    private CardView view;

    // エフェクトに関する操作
    public CardEffectGenerator effectGenerator { get; private set; }

    private void Awake()
    {
        view = GetComponent<CardView>();
        effectGenerator = GetComponent<CardEffectGenerator>();
    }

    // 初期化する。
    public void Init(int cardId, GameSide side, CardPlace place)
    {
        model = new CardModel(cardId, side, place);
    }

    // 攻撃する。
    public void Attack(CardController card, Direction direction)
    {
        model.Attack(card, direction);
    }

    // HPを回復する。
    public void RecoverHp(int amount)
    {
        model.RecoverHp(amount);
    }
    // 攻撃力を増加する。
    public void IncreaseAttackPoint(Direction d, int amount)
    {
        model.IncreaseAttackPoint(d, amount);
    }
    // 攻撃力を減少する。
    public void DecreaseAttackPoint(Direction d, int amount)
    {
        model.DecreaseAttackPoint(d, amount);
    }

    // 死亡チェック
    public bool IsDeath()
    {
        return model.CheckDeath();
    }

    // 死亡処理
    public void Death()
    {
        effectGenerator.CreateDeathEffect();
        Destroy(this.gameObject);
    }

    public void ExtendCardParameterAnimation()
    {
        view.ExtendCardParameterAnimation();
    }

    public void Update()
    {
        model.CheckCanMoveToField();
        view.Draw(model);
    }

    // エフェクトを発動する。
    public void UseSkill(Skill.Timing timing)
    {
        model.UseSkill(this, timing);
    }
}