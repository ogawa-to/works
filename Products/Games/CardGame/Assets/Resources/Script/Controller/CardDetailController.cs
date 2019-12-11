using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カード詳細コントローラ
public class CardDetailController : MonoBehaviour
{
    private CardDetailModel model;
    private CardDetailView view;

    private void Awake()
    {
        view = GetComponent<CardDetailView>();
    }

    public void Init(string cardName, string detail)
    {
        model = new CardDetailModel(cardName, detail);
        view.Init();
        view.Draw(model);
        view.Animate();
    }
}
