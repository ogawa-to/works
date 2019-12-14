using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI表示
public class UIManager : MonoBehaviour
{

    [SerializeField] private Text messageInfo;
    [SerializeField] private Text connectionInfo;

    void Update()
    {

        WriteConnectState();
        getState();
    }

    public void WriteLog(string message)
    {
        messageInfo.text += "\r\n" + message;
    }

    private void WriteConnectState()
    {
        string log = getState();
        connectionInfo.text = log;
    }

    public string getState()
    {
        string log = "";
        log += "\r\n" + "connectionState : " + PhotonNetwork.connectionStateDetailed.ToString();
        log += "\r\n" + "isMasterClient : " + PhotonNetwork.isMasterClient;
        log += "\r\n" + "connected : " + PhotonNetwork.connected;
        log += "\r\n" + "issideLobby : " + PhotonNetwork.insideLobby;
        log += "\r\n" + "isRoom : " + PhotonNetwork.inRoom;
        log += "\r\n" + "【現在の部屋】";

        Room room = PhotonNetwork.room;
        if (room != null)
        {
            log += "\r\n" + "roomName : " + room.Name;
            log += "\r\n" + "connected : " + room.PlayerCount;
        }
        else
        {
            log += "\r\n" + "roomName : " + "---";
            log += "\r\n" + "connected : " + "---";
        }
        return log;
    }
}
