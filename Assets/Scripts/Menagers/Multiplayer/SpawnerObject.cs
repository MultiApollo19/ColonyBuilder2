using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColonyBuilder.Multiplayer;

namespace ColonyBuilder.Multiplayer {
    public class SpawnerObject : MonoBehaviour
    {
        public static SpawnerObject instance;

        public bool isColiding = false;

        public void Awake()
        {
            instance = this;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.name == "Player") {
                isColiding = true;
            }
        }
    }

}
