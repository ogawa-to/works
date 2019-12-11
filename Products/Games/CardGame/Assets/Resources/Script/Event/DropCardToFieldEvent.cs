using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// フィールドを場に出すイベント
public class DropCardToFieldEvent : MonoBehaviour, IDropHandler
{
    // ドロップイベント
    public void OnDrop(PointerEventData eventData)
    {
        // カードの配置先を自身に切り替える。
        CardMoveEvent cardMove = eventData.pointerDrag.GetComponent<CardMoveEvent>();
        CardController card = cardMove.GetComponent<CardController>();

        // 移動可能なカード(=手札)でなければフィールドには出せない。
        if (!cardMove.isMovable)
        {
            return;
        }

        // ヒエラルキー上の配置をフィールドに設定する。
        cardMove.defaultParent = this.transform;

        // フィールドへのカードの二重配置がされないようにイベントを無効化する。
        this.enabled = false;

        // 召喚の処理を行う。
        FieldGridController fieldGrid = GetComponent<FieldGridController>();
        StartCoroutine(GameManager.instance.Summon(card, fieldGrid));
    }
}
