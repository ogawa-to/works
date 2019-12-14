using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerId;
    public string playerName;
    public int point;
    public int hand;

    [SerializeField] private Text nameText;
    [SerializeField] private Text pointText;
    [SerializeField] private Text handText;

    void Update()
    {
        RefreshGui();
    }

    // 初期化する。
    public void Init(int playerId, string playerName)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        this.point = 0;
        this.hand = -1;
    }

    public void RefreshGui()
    {
        nameText.text = playerName;
        pointText.text = point.ToString();
        handText.text = HandToString(hand);
    }

    private string HandToString(int hand)
    {
        switch (hand)
        {
            case -1:
                return "---";
            case 0:
                return "グー";
            case 1:
                return "チョキ";
            case 2:
                return "パー";
        }
        return null;
    }
}
