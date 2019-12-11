using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMoveEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // 退避用のオブジェクト
    [HideInInspector] public Transform defaultParent;

    // 自オブジェクトのコンポーネント
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private CardController card;

    // 移動可能かどうか。
    [HideInInspector] public bool isMovable;
    // 手札での並び順
    private int handOrderIndex;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        card = GetComponent<CardController>();
        isMovable = false;
    }

    // ドラッグ開始時のイベント
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!card.model.canMoveToField)
        {
            isMovable = false;
            return;
        }
        isMovable = true;
        handOrderIndex = transform.GetSiblingIndex();
        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent, false);
        canvasGroup.blocksRaycasts = false;
    }

    // ドラッグ時のイベント
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMovable)
        {
            return;
        }
        // マウス座標をスクリーンからローカルへ変換する。
        Vector2 pos;
        RectTransform parent = transform.parent.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, eventData.position, Camera.main, out pos);
        rectTransform.localPosition = pos;
    }

    // ドラッグ終了時のイベント
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isMovable)
        {
            return;
        }
        transform.SetParent(defaultParent);
        transform.SetSiblingIndex(handOrderIndex);
        canvasGroup.blocksRaycasts = true;
        isMovable = false;
    }
}
