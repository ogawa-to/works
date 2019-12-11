using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カードに関するエフェクトを生成するクラス
public class CardEffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardDeathEffectPrefub = default;
    [SerializeField] private GameObject hpUpEffectPrefub = default;
    [SerializeField] private GameObject attackPointUpEffectPrefub = default;


    // 死亡エフェクトを生成する。
    public void CreateDeathEffect()
    {
        GameObject g = Instantiate(cardDeathEffectPrefub, transform.parent, false);
        DelayDeleteParticle(g);
    }

    // HP回復エフェクトを生成する。
    public void CreateHpUpEffect()
    {
        GameObject g = Instantiate(hpUpEffectPrefub, transform.parent, false);
        DelayDeleteParticle(g);
    }

    // 攻撃力アップエフェクトを生成する。
    public void CreateAttackPointUpEffect()
    {
        GameObject g = Instantiate(attackPointUpEffectPrefub, transform.parent, false);
        DelayDeleteParticle(g);
    }

    // パーティクルを遅延削除する。
    private void DelayDeleteParticle(GameObject g)
    {
        ParticleSystem particle = g.GetComponent<ParticleSystem>();
        // 再生後、オブジェクトを削除する。
        Destroy(g, particle.main.duration);
    }
}
