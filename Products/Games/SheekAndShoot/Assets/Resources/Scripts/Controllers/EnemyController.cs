using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 移動パターン
    public MovePattern mMovePattern { private get; set; }

    // ショットパターン
    public Shoot mShoot {private get; set;}
    public float mHitRange { get; set; }

    public int mMoveType { get; set; }
    public int mShootType { get; set; }
    public EnemyGenerator.Kind mKind { get; set; }
    public int mHp { get; set; }

    // ステルス時間
    public const int APPEAR_COUNT = 120;
    private const int UNTIL_HIDDEN_COUNT = 240;
    private const int HIDDEN_COUNT = 120;

    public int mAppearCount;
    public float mThrethold;
    public bool mAppearFlag;

    public Rect mRect;
    public int mCount { get; set; }

    public float mSpeed;
    public float mDegree;

    public float mBulletInfo1;

    // 死亡フラグ
    public bool mIsDeleted { get; set; }

    public void Start()
    {
        mAppearCount = 0;
        mCount = 0;
        mThrethold = 1.0f;
        mAppearFlag = false;
        mIsDeleted = false;
    }

    public void Update()
    {
        if (mIsDeleted)
        {
            return;
        }
        // 出現／消失処理を行う。
        AppearDissPose();

        // 移動計算を行う。
        mMovePattern.Move();

        // ショット計算を行う。
        mShoot.Calculate();

        mCount++;
    }

    // シーカにより見つかる。
    public void Found()
    {
        mAppearFlag = true;
    }

    // ダメージ処理を行う。
    public void Damage(int power)
    {
        mHp -= power;
        if (mHp <= 0)
        {
            Death();
        }
    }

    // 死ぬ際の処理を行う。
    public void Death()
    {
        // 画像だけ先に消し，爆発パーティクルを再生する。
        if (mHp <= 0) {
            Destroy(GetComponent<SpriteRenderer>());
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        mIsDeleted = true;
    }

    private void AppearDissPose()
    {
        if (mAppearFlag)
        {
            mAppearCount++;
            // 徐々に現れる。
            if (mAppearCount <= APPEAR_COUNT)
            {
                mThrethold = 1.0f - 1.0f * mAppearCount / APPEAR_COUNT;
            }
            /*
            // 徐々に消え始める。 再度消えるのはくそげーになってしまうため、やめ。
            } else if ((APPEAR_COUNT + UNTIL_HIDDEN_COUNT) 
                <= mAppearCount && mAppearCount < APPEAR_COUNT + UNTIL_HIDDEN_COUNT + HIDDEN_COUNT)
            {
                mThrethold = 1.0f * (mAppearCount - (APPEAR_COUNT + UNTIL_HIDDEN_COUNT)) / HIDDEN_COUNT;
            // 消えた。
            } else if (mAppearCount == APPEAR_COUNT + UNTIL_HIDDEN_COUNT + HIDDEN_COUNT) {
               mAppearFlag = false;
               mAppearCount = 0;
            }
            */
        }

        // ステルス度合の閾値を設定する。
        Material m = GetComponent<Renderer>().material;
        if (GetComponent<Renderer>().material.HasProperty("_Threshold"))
        {
            GetComponent<Renderer>().material.SetFloat("_Threshold", mThrethold);
        }
    }
}
