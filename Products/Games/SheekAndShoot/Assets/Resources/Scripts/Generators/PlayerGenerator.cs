using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤを生成するプレハブクラス
public class PlayerGenerator : MonoBehaviour
{
    public GameObject playerPrefub;
    BulletManager bulletManager;
    Sprite[] mSprites;
    const int PLAYER_NUM = 1;

    void Awake()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        LoadResources();
    }

    void Start()
    {
    }

    // リソースを読み込む。
    void LoadResources()
    {
        mSprites = new Sprite[PLAYER_NUM];
        mSprites[0] = Resources.Load<Sprite>("Images/Player/Player001");
    }

    public PlayerController Create()
    {
        PlayerController player = Instantiate(playerPrefub).GetComponent<PlayerController>();
        player.transform.Translate(20.0f, 20.0f, 1.0f);
        player.mHitRange = 2.0f;
        player.mShoot = new PlayerShoot_001(bulletManager, player.gameObject);


        player.GetComponent<SpriteRenderer>().sprite = mSprites[0];
        player.mRect = mSprites[0].rect;

        return player.GetComponent<PlayerController>();
    }
}
