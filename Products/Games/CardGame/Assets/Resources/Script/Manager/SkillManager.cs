using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public  bool IsMatchTiming(Skill skill, Skill.Timing timing)
    {
        return skill.timing == timing;
    }

    // スキルの対象一覧を取得する。
    // 現時点ではカードのみ対象とする。
    public List<CardController> GetTargetList (CardController card, Skill skill)
    {
        FieldController field = GameManager.instance.fieldController;
        List<CardController> cardList = new List<CardController>();

        // 場所の指定
        switch(skill.target)
        {
            case Skill.Target.Myself:
                cardList.Add(card);
                break;
            case Skill.Target.Unit_Arround:
                foreach (var keyValue in FieldController.AllAroundDirection) {
                    cardList.Add(field.GetArroundCard(card, keyValue.Value));
                }
                break;
            case Skill.Target.Unit_U:
                cardList.Add(field.GetArroundCard(card, FieldController.AllAroundDirection[Direction.UP]));
                break;
            case Skill.Target.Unit_D:
                cardList.Add(field.GetArroundCard(card, FieldController.AllAroundDirection[Direction.DOWN]));
                break;
            case Skill.Target.Unit_L:
                cardList.Add(field.GetArroundCard(card, FieldController.AllAroundDirection[Direction.LEFT]));
                break;
            case Skill.Target.Unit_R:
                cardList.Add(field.GetArroundCard(card, FieldController.AllAroundDirection[Direction.RIGHT]));
                break;
        }

        // 存在しない箇所を除外
        cardList = cardList.FindAll(c => c != null);
        // 味方敵の指定
        cardList = cardList.FindAll(c => (c.model.gameSide == skill.gameSide));

        return cardList;
    }
}
