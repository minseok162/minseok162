using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chating : MonoBehaviourPunCallbacks
{
    public Text[] ChatText;
    public InputField textChat;
    bool bEnter = false;
    public PhotonView PV;

    void Awake()
    {
        PhotonNetwork.GameVersion = "v1.0";
        var request = new GetUserDataRequest() { PlayFabId = Variables.name };
        PlayFabClientAPI.GetUserData(request, (result) => PhotonNetwork.NickName= result.Data["NAME"].Value, (error) => print("데이터 불러오기 실패"));
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("방 입장");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode}, msg={message}");
        PhotonNetwork.CreateRoom("Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }

    void EnterChat()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + ":" + textChat.text);
        textChat.text = string.Empty;
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))    //엔터치면 인풋 필드 활성화
        {
                    EnterChat();
        }
    }

}
