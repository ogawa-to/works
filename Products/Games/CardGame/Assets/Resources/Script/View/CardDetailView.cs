using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardDetailView : MonoBehaviour
{
    [SerializeField] private Image panel = default;
    [SerializeField] private Text cardNameText = default;
    [SerializeField] private Text detailText = default;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init()
    {
        canvasGroup.alpha = 0.0f;
    }

    public void Draw(CardDetailModel model)
    {
        cardNameText.text = model.cardName;
        detailText.text = model.detail;
    }

    // 出現モーション
    public void Animate()
    {
        // パネルとその配下の透過度を一律替えるにはcanvasGroupを使用する。
        canvasGroup.DOFade(1.0f, 0.5f);
        panel.rectTransform.DOLocalMoveX(5, 0.5f);
    }
}
