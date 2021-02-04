using System;
using UnityEngine;

namespace ColonyBuilder.SaveLoad
{
    public class OptionsSystem : MonoBehaviour, ISaveable
    {
        public static OptionsSystem instance;

        [SerializeField] public string nickName = "Guest000";



        public void Awake()
        {
            instance = this;
        }
        public object CaptureState()
        {
            return new SaveData
            {
                nickName = nickName
            };
        }

        public void SetNickName(string _nickName)
        {
            nickName = _nickName;
            print("Current nickname: " + _nickName);
        }
        public void RestoreState(object state)
        {
            var saveData = (SaveData)state;
            nickName = saveData.nickName;
        }

        [Serializable]
        private struct SaveData {
            public string nickName;
        }
    }
}
