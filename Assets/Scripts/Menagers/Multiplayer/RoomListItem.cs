using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using ColonyBuilder.Multiplayer;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;

    public RoomInfo info;
    public void SetUp(RoomInfo _info) {
        info = _info;
        roomName.text = _info.Name;
    }
    public void OnClick() {
        NetworkMenager.Instance.JoinRoom(info);
        print(this.name);
    }
}
