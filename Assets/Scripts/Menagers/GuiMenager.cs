using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ColonyBuilder.Multiplayer;
using TMPro;

namespace ColonyBuilder.UI.game
{
    public class GuiMenager : MonoBehaviour
    {
        public static GuiMenager Instance;    

        [SerializeField] private GameObject EscPanel;

        PhotonView photonView;
        private void Awake()
        {
            Instance = this;            
        }
        public void escPanelSet(bool isESC)
        {
            if (isESC)
            {
                EscPanel.SetActive(true);
            }
            else
            {
                EscPanel.SetActive(false);
            }
        }
        public void leaveGame()
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }
    }
}
