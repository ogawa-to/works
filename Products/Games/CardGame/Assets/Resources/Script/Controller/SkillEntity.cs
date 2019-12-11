using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルの情報をUnityで保持する。
// インスペクタでの設定はCardEntity側のScriptableObjectで
// 一括で行う。
[System.Serializable]
public class SkillEntity
{
    public GameSide gameSide;
    public Skill.Target target;
    public Skill.Timing timing;
    public Skill.Type type;
    public int lifeTimeSpan;
    public List<int> argument;
}