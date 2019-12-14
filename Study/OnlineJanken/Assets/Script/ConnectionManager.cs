using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PUNのコールバック関数を実装する。
public class ConnectionManager : Photon.MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] GameManager gameManager;

    [SerializeField] Text pauseCounter;

    // ネットワーク関連
    public bool isEnterRoom;

    public void Start()
    {
        isEnterRoom = false;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(null);
        uiManager.WriteLog("■サーバへ接続");
    }

    // ロビーへログインした。
    public void OnConnectedToMaster()
    {
        
        if (PhotonNetwork.isMasterClient)
        {
            uiManager.WriteLog("■マスタークライアントとして接続しました。");
        }
        else
        {
            uiManager.WriteLog("■一般クライアントとして接続しました。");
        }
        // 適当な部屋へ入室する。
        PhotonNetwork.JoinRandomRoom();
    }

    
    // autoLobbyEntryにチェックが入っていない場合は呼ばれない。
    // 一般クライアントとしてログイン
    public void OnJoinedLobby()
    {
        uiManager.WriteLog("■一般クライアントとして接続しました。");
        PhotonNetwork.JoinRandomRoom();
    }

    // ルームへ入室した。
    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            gameManager.AddPlayer(0, "Master");
        }
        uiManager.WriteLog("■Roomへ入室しました。");
        isEnterRoom = true;

    }

    // ルームの入室に失敗
    // ルームが存在しない　または 人数が上限に達している場合の処理
    public void OnPhotonRandomJoinFailed()
    {
        uiManager.WriteLog("■Roomの入室に失敗。新しいRoomを作成します。");
        PhotonNetwork.CreateRoom("Test Room1");
    }

    // ルームが作成された。
    public void OnCreatedRoom()
    {
        uiManager.WriteLog("■Roomが作成されました。");
        PhotonNetwork.JoinRandomRoom();
    }

    // ルームに別の誰かが入ってきた場合
    public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        // マスタークライアントのみ処理する。
        if (PhotonNetwork.isMasterClient)
        {
            // 新規プレイヤーへIDを付与。
            int playerId = UnityEngine.Random.Range(0, 9999999);
            gameManager.myView.RPC("SetPlayerId", newPlayer, new object[] { playerId });

            // 既に入室しているプレイヤー情報を新規プレイヤーに追加する。
            foreach (PlayerController p in gameManager.GetPlayers())
            {
                gameManager.myView.RPC("AddPlayer", newPlayer, new object[] { p.playerId, p.playerName });
            }
            // 全ての入室しているプレイヤーに追加されたプレイヤ－情報を追加する。
            gameManager.myView.RPC("AddPlayer", PhotonTargets.All, new object[] { playerId, "new Player" });
        }
    }

    
    public void OnApplicationPause(bool pause)
    {
       if (pause == true)
        {
            pauseCounter.text += "スリープ" + Time.time + "\r\n";
        } else
        {
            pauseCounter.text += "解除" + Time.time + "\r\n";
        }
    }

}
