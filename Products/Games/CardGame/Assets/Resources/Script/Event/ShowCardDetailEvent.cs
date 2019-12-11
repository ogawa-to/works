using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// カード詳細表示イベント
public class ShowCardDetailEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDetailController cardDetailPrefub = default;
    private Transform canvasTransform;

    private void Awake()
    {
        // プレハブへのTransformのアタッチはできないため、
        // Findで動的に取得する。
        GameObject g = GameObject.FindGameObjectWithTag("Canvas");
        canvasTransform = g.GetComponent<Transform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 既に表示している場合、一度削除する。
        GameObject cardDetail = GameObject.FindGameObjectWithTag("CardDetail");
        if (cardDetail != null)
        {
            Destroy(cardDetail);
        }

        // 表示内容を初期化する。
        CardDetailController cardDetailController; 
        cardDetailController = Instantiate(cardDetailPrefub, canvasTransform, false);
        CardController card = GetComponent<CardController>();
        cardDetailController.Init(card.model.name, card.model.text);
    }
}
