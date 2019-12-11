using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// スキル効果の生成
class SkillEffectFactory
{
    public static ISkillEffect Create(Skill.Type type)
    {
        switch(type)
        {
            case Skill.Type.HpAttackUp:
                return new HpAttackUpSkillEffect();
            case Skill.Type.Damage:
            case Skill.Type.Kill:
            case Skill.Type.MoveUp:
                throw new System.NotImplementedException("実装されていないスキルがあります。スキル : " + type.ToString());
            default:
                throw new System.ArgumentOutOfRangeException("予期せぬスキルが設定されました。スキル : " + type.ToString());
        }
    }
}
