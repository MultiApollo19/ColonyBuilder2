using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using ColonyBuilder.Multiplayer;
using ColonyBuilder.SaveLoad;
using ColonyBuilder.Utils;

namespace ColonyBuilder.UI {
    public class MenuGuiMenager : MonoBehaviour
    {
        public static MenuGuiMenager Instance;

        [SerializeField] GameObject startGameButton;
        [SerializeField] GameObject nickNameWindow; //setnick/guest
        [SerializeField] GameObject MainMenu;
        
        //build
        [SerializeField] TMP_Text buildNrText;
        [SerializeField] TMP_Text nickNameText; //window
        [SerializeField] TMP_InputField nickNameInputOptions; // opcyje

        //ROOM/PLAYER list
        [SerializeField] TMP_InputField roomNameInputField;
        [SerializeField] TMP_Text roomNameText;
        [SerializeField] GameObject roomListPrefab;
        [HideInInspector] public Transform roomListContent;
        [SerializeField] GameObject playerListPrefab;
        [HideInInspector] public Transform playerListContent;

        [SerializeField] TMP_InputField nickNameInput;//window

        private bool isNetworkMenagerActive = false;
        private bool isFindingRoom = false;

        [SerializeField] public string nickName;
        [SerializeField] private string defaultNick = "Guest";

        private void Awake()
        {
            Instance = this;
            ResourceRequest request = Resources.LoadAsync("Build", typeof(BuildScriptableObject));
            request.completed += Request_completed;
        }
        public void Start()
        {

            if (!File.Exists(SavingLoadMenager.instance.SavePath))
            {
                MainMenu.SetActive(false);
                nickNameWindow.SetActive(true);
            }
            else
            {
                SavingLoadMenager.instance.Load();               
                PhotonNetwork.NickName = OptionsSystem.instance.nickName;
                nickNameText.text = "Logged as: " + PhotonNetwork.LocalPlayer.NickName;
                print(PhotonNetwork.LocalPlayer.NickName);
                isNetworkMenagerActive = true;
            }
        }
        public void Update()
        {
            if (isNetworkMenagerActive)
            {
                if (NetworkMenager.Instance.isInRoom)
                {
                    roomNameText.text = NetworkMenager.Instance.roomName;
                    startGameButtonActivate();
                }
            }

        }

        private void Request_completed(AsyncOperation obj)
        {
            BuildScriptableObject buildScriptableObject = ((ResourceRequest)obj).asset as BuildScriptableObject;
            if (buildScriptableObject == null)
            {
                Debug.LogError("Build scriptable object not found in resources dir!");
            }
            else {
                print(buildScriptableObject.BuildNumber);
                buildNrText.text = $"Version: {Application.version}.{buildScriptableObject.BuildNumber}";
            }
            
        }

        public void CreateRoom()
        {
            NetworkMenager.Instance.CreateRoom(roomNameInputField.text);
        }


        public void closeGame()
        {
            Application.Quit();
        }
        public void updateRoomList(List<RoomInfo> roomInfos)
        {

            print(roomInfos.Count);

            for (int i = 0; i < roomInfos.Count; i++)
            {
                if (roomInfos[i].RemovedFromList) {
                    continue;
                }
                Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomInfos[i]);
            }
        }
        public void updatePlayerList(Player newPlayer) {

            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);

        }
        public void startGameButtonActivate() {
            startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }
        public void SaveNickNameOptions() {
            nickName = nickNameInputOptions.text;
            if (string.IsNullOrEmpty(nickName))
            {
                print("Pusty nick");
                return;
            }
            OptionsSystem.instance.SetNickName(nickName);
            SavingLoadMenager.instance.Save();
            nickNameText.text = "Logged as: " + nickName;
            print(PhotonNetwork.LocalPlayer.NickName);
            PhotonNetwork.NickName = nickName;
        }
        public void SaveNickNameWindow() {
            nickName = nickNameInput.text;
            if (string.IsNullOrEmpty(nickName))
            {
                print("Pusty nick");
                return;
            }
            OptionsSystem.instance.SetNickName(nickName);
            SavingLoadMenager.instance.Save();
            nickNameText.text = "Logged as: " + nickName;
            PhotonNetwork.NickName = nickName;
            nickNameWindow.SetActive(false);
            MainMenu.SetActive(true);
            isNetworkMenagerActive = true;
        }
        public void continueAsGuest() {
            int value = Random.Range(0, 999);
            nickName = defaultNick + value;
            print("Po powinno być: " + nickName);
            PhotonNetwork.NickName = nickName;
            nickNameText.text = PhotonNetwork.LocalPlayer.NickName;
            nickNameWindow.SetActive(false);            
            MainMenu.SetActive(true);
            isNetworkMenagerActive = true;
        }
    }

}
