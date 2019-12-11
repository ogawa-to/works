using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// カードの外見
public class CardView : MonoBehaviour
{
    [SerializeField] private Image back = default;    [SerializeField] private Image illust = default;    [SerializeField] private Image costGage = default;    [SerializeField] private Text attackUpText = default;    [SerializeField] private Text attackDownText = default;    [SerializeField] private Text attackLeftText = default;    [SerializeField] private Text attackRightText = default;    [SerializeField] private Text costText = default;
    [SerializeField] private Text hpText = default;
    // UIを反映する。
    public void Draw(CardModel model)
    {
        illust.sprite = model.illust;
        costGage.fillAmount = model.waitAmount;
        attackUpText.text = model.attackPoints[(int)Direction.UP].ToString();
        attackDownText.text = model.attackPoints[(int)Direction.DOWN].ToString();
        attackLeftText.text = model.attackPoints[(int)Direction.LEFT].ToString();
        attackRightText.text = model.attackPoints[(int)Direction.RIGHT].ToString();
        costText.text = model.cost.ToString();
        hpText.text = model.hp.ToString();
    }

    // カードのUIを移動するアニメーション
    public void ExtendCardParameterAnimation()
    {
        costText.gameObject.SetActive(false);

        // テストコード
        RectTransform transform = back.rectTransform;
        Vector2 upPos = transform.localPosition + new Vector3(3, 0, 0);

        //attackUpText.rectTransform.localPosition = transform.localPosition + new Vector3(100, 0, 0);
        //attackDownText.rectTransform.localPosition = transform.localPosition + new Vector3(200, -200, 0);
        //attackLeftText.rectTransform.localPosition = transform.localPosition + new Vector3(0, -100, 0);
        //attackRightText.rectTransform.localPosition = transform.localPosition + new Vector3(200, -100, 0);

        // TODO;
    }
}
