using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// ヒーロースキルのアニメーションの操作
public class HeroSkillAnimationController : MonoBehaviour
{
    // アニメーション時間
    public const float ANIMATION_TIME = 10f;

    // キャラクタの画像
    [SerializeField] private Image characterImage = default;

    public IEnumerator Animate()
    {
        // 徐々に左に移動する。
        Vector3 pos = characterImage.rectTransform.localPosition - new Vector3(600, 0, 0);
        characterImage.rectTransform.DOLocalMove(pos, ANIMATION_TIME - 0.3f);
        // 10秒したらアニメーションを終える。
        yield return new WaitForSeconds(ANIMATION_TIME);
    }
}
