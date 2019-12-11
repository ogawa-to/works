using System.Collections.Generic;

// スキル効果のインターフェース
public class HpAttackUpSkillEffect : ISkillEffect
{
    // 
    public void Use(CardController user,  Skill skill)
    {
        // 対象のリストを取得する。
        SkillManager skillManager = GameManager.instance.skillManager;
        List<CardController> targetList = skillManager.GetTargetList(user, skill);

        foreach (CardController target in targetList)
        {
            int hp = skill.argument[0];
            int attackPoint = skill.argument[1];

            // HPを回復する。
            if (hp > 0)
            {
                target.RecoverHp(hp);
                target.effectGenerator.CreateHpUpEffect();
            }
            // 四方の攻撃力を増加させる。
            if (attackPoint > 0)
            {
                for (int i = 0; i < (int)Direction.NUM; i++)
                {
                    target.IncreaseAttackPoint((Direction)(i), attackPoint);
                }
                target.effectGenerator.CreateAttackPointUpEffect();
            }
        }
    }
}
