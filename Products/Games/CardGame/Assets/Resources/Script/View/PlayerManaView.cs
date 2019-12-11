using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// マナの外見
public class PlayerManaView : MonoBehaviour
{
    [SerializeField] private Text manaText = default;
    [SerializeField] private Image manaGageImage = default;

    // 描画する。
    public void Draw(PlayerManaModel model)
    {
        manaText.text = ((int)(model.manaAmount)).ToString();
        manaGageImage.fillAmount = model.manaAmount / PlayerManaModel.MANA_MAX_AMOUNT;
    }
}
