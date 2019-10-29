using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 弾幕の種類
public class PlayerShoot_001 : Shoot
{
    PlayerController player;
    AudioSource source;
    GameObject gameObject;
    public PlayerShoot_001(BulletManager manager, GameObject owner) : base(manager, owner)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Init()
    {
        gameObject = new GameObject();
        source = GameObject.Find("GameDirector")
                .GetComponent<GameDirector>().GetSe(gameObject, "playerShoot");
    }

    public override void Calculate()
    {
        if (mCount == 0)
        {
            Init();
        }
        // 弾生成
        Vector3 pos = player.transform.position;

        int num = player.mPower <=  20 ? 2 : 4;
        int[] xPos = { -25, 25, -75, 75 };
        // 2発または4発生成する。
        for (int i = 0; i < num; i++)
        {
            int id = Entry(1, 0);
            if (id != -1)
            {
                mBullets[id].mPosition = new Vector2(pos.x + xPos[i], pos.y);
                mBullets[id].mDegree = 0;
                mBullets[id].mSpeed = 14.0f;
                mBullets[id].mOwner = Owner.Player;
                mBullets[id].mPower = player.mPower;
            }
            source.Play();
        }

        mCount++;
    }
}
