using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アタックゲージのモデル
public class AttackGageModel
{
    // 攻撃間隔
    private const float ATTACK_INTERVAL = 10.0f;

    public CardController card;

    private float attackGageAmount;

    public void Init()
    {
        resetAttackGage();
    }

    // 攻撃をチャージする。
    public void IncreaseAttackGage()
    {
        attackGageAmount += Time.deltaTime;
        if (attackGageAmount >= ATTACK_INTERVAL)
        {
            Debug.Log("攻撃するぜ");
            // 攻撃を行う。
            FieldController.instance.CardBattle(card);
            resetAttackGage();
        }
    }

    // 攻撃をリセットする。
    public void resetAttackGage()
    {
        attackGageAmount = 0.0f;
    }

    // 攻撃のチャージ率を取得する。
    public float getAttackGageRatio()
    {
        return attackGageAmount / ATTACK_INTERVAL;
    }
}
