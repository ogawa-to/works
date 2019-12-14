using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameManager : Photon.MonoBehaviour
{
    [SerializeField] PlayerController playerPrefub;
    [SerializeField] Text gameStateText;

    [SerializeField] UIManager uiManager;
    [SerializeField] ConnectionManager connectionManager;

    public PhotonView myView;
    GameObject playerRoot;
    int decidedCount;
    public int myPlayerId;
    private float judgeTime;
    int winnerPlayerId;
    bool isFirst = false;

    public enum GameState
    {
        Wait,       // プレイヤ待機
        Select,     // カード選択
        Judge,      // カード判定
        Result,     // 結果発表
        End         // ゲーム終了
    }

    public GameState gameState;



    void Start()
    {
        myView = GetComponent<PhotonView>();
        gameState = GameState.Wait;
        playerRoot = GameObject.FindGameObjectWithTag("Player");
        gameStateText = GameObject.Find("GameStateText").GetComponent<Text>();
        connectionManager.Connect();
    }

    void Update()
    {
        if (connectionManager.isEnterRoom)
        {
            switch (gameState)
            {
                case GameState.Wait:
                    gameStateText.text = "State:Wait";
                    // 3人揃ったら
                    if (PhotonNetwork.room.PlayerCount == 3)
                    {
                        // ゲームを開始する。
                        GameObject.Find("WaitPanel").SetActive(false);
                        gameState = GameState.Select;
                    }
                    break;
                case GameState.Select:
                    gameStateText.text = "State:Select";
                    if (PhotonNetwork.isMasterClient)
                    {
                        // 全員手を決定したら
                        if (decidedCount == 3)
                        {
                            if (PhotonNetwork.isMasterClient)
                            {
                                // 全員自分の手を公開する。
                                myView.RPC("OpenHand", PhotonTargets.All);
                                judgeTime = 3.0f;
                                decidedCount = 0;
                                myView.RPC("SetGameState", PhotonTargets.MasterClient, new object[] { (int)GameState.Judge });
                            }
                        }
                    }
                    break;
                case GameState.Judge:
                    gameStateText.text = "State:Judge\r\n";
                    if (PhotonNetwork.isMasterClient)
                    {
                        judgeTime -= Time.deltaTime;
                        // 最初のフレームだけ
                        if (isFirst)
                        {
                            // 勝者を決める。
                            winnerPlayerId = 0;
                            // 点数加算を通知する。
                            myView.RPC("PointPlus", PhotonTargets.All, winnerPlayerId);
                            isFirst = false;
                        }
                        if (judgeTime < 0.0f)
                        {
                            // 次の手へ
                            if (GetPlayer(winnerPlayerId).point < 3)
                            {
                                myView.RPC("SetGameState", PhotonTargets.All, new object[] { (int)GameState.Select });
                                foreach (PlayerController info in GetPlayers())
                                {
                                    info.hand = -1;
                                }
                            }
                            else
                            {
                                myView.RPC("SetGameState", PhotonTargets.All, new object[] { (int)GameState.Result });
                            }
                        }
                    }
                    break;
                case GameState.Result:
                    gameStateText.text = "State:Result";
                    break;

            }
        }
    }

    // 全てのプレイヤ情報を取得する。
    public PlayerController[] GetPlayers()
    {
        PlayerController[] players = playerRoot.GetComponentsInChildren<PlayerController>();
        return players;
    }

    // プレイヤ情報を取得する。
    public PlayerController GetPlayer(int playerId)
    {
        PlayerController[] players = playerRoot.GetComponentsInChildren<PlayerController>();
        PlayerController player = Array.Find(players, p => p.playerId == playerId);
        return player;
    }

    // プレイヤを追加する。
    [PunRPC]
    public void AddPlayer(int playerId, string playerName)
    {
        uiManager.WriteLog("プレイヤ【" + playerName + "】を追加します。");
        PlayerController playerController = Instantiate(playerPrefub, GameObject.FindGameObjectWithTag("Player").transform, false);
        playerController.Init(playerId, playerName);
    }

    [PunRPC]
    // 手を設定する。
    public void OpenHand()
    {
        // 自分以外に自分の手を通知する。
        myView.RPC("SetHand", PhotonTargets.Others, new object[] {myPlayerId, GetPlayer(myPlayerId).hand});
    }

    [PunRPC]
    public void SetHand(int playerId, int hand)
    {
        // 自分の奴のカードを取得して、handを通知する。
        uiManager.WriteLog("プレイヤ【" + GetPlayer(playerId).playerName + "】の手は【" + hand + "】です。");
        GetPlayer(playerId).hand = hand;
    }

    [PunRPC]
    // 自身のプレイヤーIDを設定する。
    public void SetPlayerId(int playerId)
    {
        myPlayerId = playerId;
    }

    [PunRPC]
    public void DecidedHand(int playerId)
    {
        uiManager.WriteLog("プレイヤ【" + GetPlayer(playerId).playerName + "】は手を決定した。");
        decidedCount++;
    }

    [PunRPC]
    public void PointPlus(int playerId)
    {
        GetPlayer(playerId).point++;
    }

    [PunRPC]
    public void SetGameState(int gameState)
    {
        this.gameState = (GameState)gameState;
        isFirst = true;
    }
}
