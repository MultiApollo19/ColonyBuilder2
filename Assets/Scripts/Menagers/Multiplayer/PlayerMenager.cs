using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using ColonyBuilder.Multiplayer;

public class PlayerMenager : MonoBehaviour
{
    PhotonView photonView;


    
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (photonView.IsMine)
        {
            CreateControler();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    void CreateControler() {
        Vector3 spawnPosition = new Vector3();
        spawnPosition = SpawnerMenager.Instance.getSpawnerPosition();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPosition, Quaternion.identity);
    }
}
