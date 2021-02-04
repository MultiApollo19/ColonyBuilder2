using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using ColonyBuilder.Utils;
using ColonyBuilder.UI;
using ColonyBuilder.SaveLoad;

namespace ColonyBuilder.Multiplayer {
    public class NetworkMenager : MonoBehaviourPunCallbacks
    {
        public static NetworkMenager Instance;

        public bool isConnected = false;
        public bool isInRoom = false;
        public int netPing = 0;
        public string nickName = null;

        [SerializeField] [HideInInspector] private float timer = 0;
        [HideInInspector] public string roomName = null;



        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {       
            PhotonNetwork.NickName = OptionsSystem.instance.nickName;
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.ConnectUsingSettings();
        }
        private void Update()
        {
            
        }
        /*void updatePing() {
            

            while (!isConnected) {
                return;
            }
            timer += Time.deltaTime;
            if (timer >= 1) {
                netPing = PhotonNetwork.GetPing();
                timer = 0;
            }
            
        }*/
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;

        }
        public override void OnJoinedLobby()
        {
            isConnected = true;
            nickName = PhotonNetwork.LocalPlayer.NickName;
        }
        public void StartGame() {
            PhotonNetwork.LoadLevel(1);
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError("Disconnected because " + cause);
        }
        public void CreateRoom(string roomName) {
            if (string.IsNullOrEmpty(roomName)) {
                return;
            }
            PhotonNetwork.CreateRoom(roomName);
        }
        public void DeleteRoom() {
            PhotonNetwork.LeaveRoom();
        }
        public void JoinRoom(RoomInfo info) {
            PhotonNetwork.JoinRoom(info.Name);


        }
        public override void OnJoinedRoom()
        {
            isInRoom = true;
            roomName = PhotonNetwork.CurrentRoom.Name;
            MenuMenager.Instance.OpenMenu("roomMenu");

            Player[] players = PhotonNetwork.PlayerList;

            foreach (Transform child in MenuGuiMenager.Instance.playerListContent)
            {
                Destroy(child.gameObject);
            }


            for (int i = 0; i < players.Count(); i++)
            {
                MenuGuiMenager.Instance.updatePlayerList(players[i]);
            }
        }
        public override void OnLeftRoom()
        {
            isInRoom = false;
            roomName = null;
            MenuMenager.Instance.OpenMenu("main");
        }
        /*public override void OnCreateRoomFailed(short returnCode, string message)
        {
            
        }*/

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (Transform child in MenuGuiMenager.Instance.roomListContent)
            {
                Destroy(child.gameObject);

            }


            print("NetworkMenager -> OnRoomListUpdate -> roomCount -> " + roomList.Count );
            MenuGuiMenager.Instance.updateRoomList(roomList);
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            MenuGuiMenager.Instance.updatePlayerList(newPlayer);
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            MenuGuiMenager.Instance.startGameButtonActivate();
        }
    }

}
