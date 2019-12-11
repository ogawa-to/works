using System.Collections.Generic;

// スキル効果のインターフェース
public interface ISkillEffect
{
    void Use(CardController card, Skill skill);
}
