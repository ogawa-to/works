using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 敵のAIクラス
public class EnemyAIController : MonoBehaviour
{
    // 思考時間の頻度
    public const float THINK_INTERVAL = 5.0f;

    GameManager gameManager;
    public void BeginThink()
    {
        StartCoroutine(Think());
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private IEnumerator Think()
    {
        while (true)
        {
            yield return new WaitForSeconds(THINK_INTERVAL);
            // ポーズ中は処理を行わない。
            if (Pauser.isPause)
            {
                continue;
            }
            JudgeSummon();
            // JudgeUseHeroSkill();
        }
    }

    // 召喚処理
    private void JudgeSummon()
    {
        // 召喚可能なカードを探す。
        float mana = gameManager.GetPlayerMana(GameSide.Enemy).GetComponent<PlayerManaController>().model.manaAmount;
        CardController[] hands = gameManager.enemyHandTransform.GetComponentsInChildren<CardController>();
        CardController[] summonableHands = Array.FindAll(hands, hand => hand.model.cost < mana);

        // フィールドの空き場所を探す。
        FieldGridController[] emptyGrids = gameManager.fieldController.GetEmptyGrid();

        // 召喚可能であれば召喚する。
        if (summonableHands.Length > 0 && emptyGrids.Length > 0)
        {
            StartCoroutine(gameManager.Summon(summonableHands[0], emptyGrids[0]));
        }
    }

    // ヒーロースキルの使用判定処理
    private void JudgeUseHeroSkill()
    {
        int skillCount = gameManager.GetSkillCount(GameSide.Enemy);
        if (skillCount > 0)
        {
            // もし自分のユニット数が相手よりも1体以上少なければ
            int myUnitCount = gameManager.fieldController.GetAllCardsCount(GameSide.Enemy);
            int oppUnitCount = gameManager.fieldController.GetAllCardsCount(GameSide.Player);
            if (myUnitCount <= oppUnitCount - 1)
            {
                StartCoroutine(gameManager.UseHeroSkill(GameSide.Enemy));
            }
        }
    }

}
