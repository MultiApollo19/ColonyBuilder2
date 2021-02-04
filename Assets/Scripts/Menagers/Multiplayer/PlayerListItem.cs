using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ColonyBuilder.Multiplayer;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] Button button;
    Player player;

    public void SetUp(Player _player) {
        
        player = _player;
        playerName.text = _player.NickName;
        masterColor();

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer) {
            Destroy(this.gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(this.gameObject);
    }
    public void masterColor() {
        if (PhotonNetwork.MasterClient.NickName == player.NickName)
        {
            button = GetComponent<Button>();
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = new Color32(15, 212, 0, 255);
            colorBlock.highlightedColor = new Color32(13, 181, 0, 255);
            colorBlock.selectedColor = new Color32(102, 255, 0, 255);
            button.colors = colorBlock;
        }

    }
    public override void OnMasterClientSwitched(Player newMasterClient) {
        masterColor();
    }
}
