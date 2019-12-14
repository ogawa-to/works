using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 手を決定するイベント
public class DecideHandEvent : MonoBehaviour
{
    GameManager gameManager;
    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnDecided(int hand)
    {
        gameManager.GetPlayer(gameManager.myPlayerId).hand = hand;
        gameManager.myView.RPC("DecidedHand", PhotonTargets.MasterClient, gameManager.myPlayerId);
    }
}
