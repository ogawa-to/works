using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マナのモデル
public class PlayerManaModel
{
    // マナの総量
    private float _manaAmount;
    public float manaAmount { get { return _manaAmount; } }

    // 秒間のマナのチャージ速度
    private const float INCREASE_VOLUME = 0.25f;
    public const float MANA_MAX_AMOUNT = 10.0f;

    public void Init(float amount)
    {
        _manaAmount = amount;
    }

    // マナを消費する。
    public void ConsumeMana(int cost)
    {
        _manaAmount -= cost;
    }

    // マナを増加させる。
    public void IncreaseMana()
    {
        _manaAmount += INCREASE_VOLUME * Time.deltaTime;
        if (_manaAmount > MANA_MAX_AMOUNT)
        {
            _manaAmount = MANA_MAX_AMOUNT;
        }
    }

    // コスト以上のマナがあるかどうか。
    public bool IsEnoughMana(int cost)
    {
        return (cost >= manaAmount) ? true : false;
    }

}
