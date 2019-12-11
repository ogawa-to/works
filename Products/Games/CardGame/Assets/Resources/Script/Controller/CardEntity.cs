using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カード情報の外部データ
[CreateAssetMenu(fileName = "DefaultCardEntity", menuName = "Card Entity")]
public class CardEntity : ScriptableObject
{
    public int id;                      // ID
    public new string name;             // カード名
    public int attackPointUp;           // 四方の攻撃力
    public int attackPointDown;
    public int attackPointLeft;
    public int attackPointRight;
    public int hp;                      // HP
    public int cost;                    // コスト
    public string text;                 // カードの説明文
    public Sprite illust;               // 画像

    public List<SkillEntity> skillEntityList; // スキルのリスト
}
