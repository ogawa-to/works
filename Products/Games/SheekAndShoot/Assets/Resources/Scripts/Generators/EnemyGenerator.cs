using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵を生成するプレハブクラス
public class EnemyGenerator : MonoBehaviour
{ 

    public enum Kind
    {
        ENEMY_001 = 0
        , ENEMY_002
        , ENEMY_003
        , ENEMY_004
    }

    public GameObject enemyPrefub;
    private Sprite[] mSprites;
    private int ENEMY_KIND_MAX = 4;

    void Awake()
    {
        LoadResources();
    }

    // リソースを読み込む。
    void LoadResources()
    {
        mSprites = new Sprite[ENEMY_KIND_MAX];
        mSprites[0] = Resources.Load<Sprite>("Images/Enemy/enemy001");
        mSprites[1] = Resources.Load<Sprite>("Images/Enemy/enemy002");
        mSprites[2] = Resources.Load<Sprite>("Images/Enemy/enemy003");
        mSprites[3] = Resources.Load<Sprite>("Images/Enemy/enemy004");
    }

    // 敵を生成する。
    public EnemyController Create(Kind kind) 
    {
        EnemyController enemy = null;
        enemy = Instantiate(enemyPrefub).GetComponent<EnemyController>();

        Sprite s = mSprites[(int)kind];
        enemy.GetComponent<SpriteRenderer>().sprite = s;
        enemy.mRect = s.rect;

        return enemy;
    }
}
