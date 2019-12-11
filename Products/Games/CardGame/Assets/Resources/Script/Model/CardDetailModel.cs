using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDetailModel
{
    public string cardName { get; }
    public string detail { get; }
    public CardDetailModel(string cardName, string detail)
    {
        this.cardName = cardName;
        this.detail = detail;
    }
}
