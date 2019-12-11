using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackGageView : MonoBehaviour
{
    [SerializeField] private Image attackGageInnerImage = default;

    public void Draw(AttackGageModel model)
    {
        attackGageInnerImage.fillAmount = model.getAttackGageRatio();
    }
}
