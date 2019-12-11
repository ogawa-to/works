using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill
{
    // スキルの対象
    public enum Target
    {
        Myself              // 自身
        , Unit_L            // 自身の上下左右のユニット
        , Unit_R    
        , Unit_U    
        , Unit_D  
        , Unit_Arround
    };

    // スキルの発動タイミング
    public enum Timing
    {
        Summon              // 召喚時 
        , Attack            // 攻撃時
    }

    // スキルの使用回数:無限
    public const int LIFETIME_SPAN_INFINITY = -1;
    
    // スキルの種類
    public enum Type
    {   None                // スキルなし
        , HpAttackUp        // ステータス向上
        , Damage            // ダメージ付与
        , Kill              // 破壊  
        , MoveUp            // 移動
    }

    public GameSide gameSide { get; private set; }
    public Target target { get; private set; }
    public Timing timing { get; private set; }
    public int lifeTimeSpan { get; private set; }
    public Type type { get; private set; }
    // スキルに必要なパラメタ
    public List<int> argument { get; private set; }

    // スキルエフェクト
    public ISkillEffect skillEffect;

    // コンストラクタ
    public Skill(SkillEntity entity)
    {
        gameSide = entity.gameSide;
        target = entity.target;
        timing = entity.timing;
        lifeTimeSpan = entity.lifeTimeSpan;
        type = entity.type;
        argument = entity.argument;
        skillEffect = SkillEffectFactory.Create(type);
    }

    // スキルの期限を迎えたかどうか。
    public bool IsEndOfLife()
    {
        if ((lifeTimeSpan != LIFETIME_SPAN_INFINITY) 
            && lifeTimeSpan <= 0)
        {
            return true;
        }
        return false;
    }

    // スキルを使用する。
    public void UseSkill(CardController user)
    {
        skillEffect.Use(user, this);
    }

}
