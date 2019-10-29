using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float MOVE_SPEED = 6.0f;
    public float mHitRange { get; set;}

    public Shoot mShoot { get; set; }
    public RippleShoot mRippleShoot { get; set; }

    // ショットの強さ
    public int mPower { get; set; }
    // リップルのチャージ速度
    public int mChargeSpeed { get; set; }

    // スタミナの最大量とスタミナの使用量
    public int mStaminaMax;
    public float mCurrentStamina;
    public int STAMINA_CHARGE_TIME = 300;

    // リップルの範囲
    public int mRippleDegree { get; set; }

    // 画像サイズ
    public Rect mRect { get; set; }

    public bool mDeathFlag = false;

    KeyUtil mKeyboard;
    FieldController mFieldController;
    void Awake()
    {
        mKeyboard = GameObject.Find("Keyboard").GetComponent<KeyUtil>();
        mFieldController = GameObject.Find("Field").GetComponent<FieldController>();
    }

    void Start()
    {
        mPower = 10;
        mChargeSpeed = 100;
        mRippleDegree = 90;
        mCurrentStamina = 100;
        mStaminaMax = 30;
    }

    void Update()
    {
        Input();
    }

    // 入力処理を行う。
    void Input()
    {

        // 移動処理
        // 上キー
        Vector2 move = Vector2.zero;
        if (mKeyboard.IsMatchKeyInterval(KeyCode.UpArrow, 1) )
        {
            move += Vector2.up;

        // 下キー
        } else if (mKeyboard.IsMatchKeyInterval(KeyCode.DownArrow, 1))
        {
            move += Vector2.down;
        }
        // 左キー
        if (mKeyboard.IsMatchKeyInterval(KeyCode.LeftArrow, 1))
        {
            move += Vector2.left;

        }
        // 右キー
        else if (mKeyboard.IsMatchKeyInterval(KeyCode.RightArrow, 1))
        {
            move += Vector2.right;
        }
        move = move.normalized;
        move *= MOVE_SPEED;
        
        // Shift(低速移動)
        if (mKeyboard.IsMatchKeyInterval(KeyCode.LeftShift, 1))
        {
            move *= 0.5f;
        }

        Vector3 beforePos = gameObject.transform.position;
        Vector3 movedPos = new Vector3(beforePos.x + move.x, beforePos.y + move.y, 0.0f);
        // 左右の壁に接していた場合
        if (mFieldController.IsOnBoundaryLeft(movedPos, mRect) || mFieldController.IsOnBoundaryRight(movedPos, mRect))
        {
            // X方向の移動をキャンセルし、Y方向のみ移動する。
            move.x = 0f;
            move = move.normalized * MOVE_SPEED;
        }
        if (mFieldController.IsOnBoundaryTop(movedPos, mRect) || mFieldController.IsOnBoundaryBottom(movedPos, mRect))
        {
            // Y方向の移動をキャンセルし、Y方向のみ移動する。
            move.y = 0f;
            move = move.normalized * MOVE_SPEED;
        }

        transform.Translate(move.x, move.y, 0.0f);

        // ショット処理
        GameObject.Find("Log").GetComponent<Log>().AddMessage(mKeyboard.GetKeyPushTime(KeyCode.Z).ToString());
        if (mKeyboard.IsMatchKeyInterval(KeyCode.Z, 5))
        {
            mShoot.Calculate();
        }
        // リップル処理
        if (mKeyboard.GetKeyPushTime(KeyCode.X) > 0)
        {
            // スタミナが残っている場合
            if (mCurrentStamina > 0.0) { 
                mCurrentStamina -= 1.0f * mStaminaMax / STAMINA_CHARGE_TIME;
            } else
            {
                mRippleShoot.Deactive();
            }
        } else
        {
            // チャージ時間かけてMAXまで回復する。
            mCurrentStamina += 1.0f * mStaminaMax / STAMINA_CHARGE_TIME;
            if (mCurrentStamina > mStaminaMax)
            {
                mCurrentStamina = mStaminaMax;
            }
            mRippleShoot.Deactive();
        }

    }

    // 死亡処理
    public void Death()
    {
        mDeathFlag = true;
    }
}
