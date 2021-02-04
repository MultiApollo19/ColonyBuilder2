using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColonyBuilder.Multiplayer {
    public class SpawnerMenager : MonoBehaviour
    {
        public static SpawnerMenager Instance;

        public List<GameObject> spawnerList;

        public void Awake()
        {
            Instance = this;
        }
        public Vector3 getSpawnerPosition() {
            Vector3 spawnerPosition = new Vector3();
            for (int i = 0; i < spawnerList.Count; i++)
            {
                if (!spawnerList[i].GetComponent<SpawnerObject>().isColiding)
                {
                    spawnerPosition = spawnerList[i].transform.position;
                    Debug.Log(spawnerPosition);
                }
            }
            return spawnerPosition;
        }
    }

}
