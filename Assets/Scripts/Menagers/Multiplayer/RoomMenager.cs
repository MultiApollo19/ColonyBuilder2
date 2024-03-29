﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

namespace ColonyBuilder.Multiplayer {
    public class RoomMenager : MonoBehaviourPunCallbacks
    {
        public static RoomMenager Instance;
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;

        }
        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
            print(Path.Combine("PhotonPrefabs", "PlayerMenager"));
        }
        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == 1)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerMenager"), Vector3.zero, Quaternion.identity);
                print("ready");
            }
        }
    }
}

