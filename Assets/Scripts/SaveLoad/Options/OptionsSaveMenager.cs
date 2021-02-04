using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

namespace ColonyBuilder.Utils {
    public class OptionsSaveMenager : MonoBehaviour
    {
        [SerializeField] private OptionsData optionsData = new OptionsData();

        public static OptionsSaveMenager instance;
        private void Awake()
        {
            instance = this;
        }

        [ContextMenu("Save")]
        private void Save() {
            //string json = JsonUtility.ToJson(optionsData);

            //File.WriteAllText($"{Application.persistentDataPath}/config.cfg",json);

            var bf = new BinaryFormatter();

            FileStream file = File.Create($"{Application.persistentDataPath}/config.cfg");

            bf.Serialize(file, optionsData);
            file.Close();
        }

        [ContextMenu("Load")]
        private void Load() {
            if (!File.Exists($"{Application.persistentDataPath}/config.cfg")) { return; }
            //string json = File.ReadAllText($"{Application.persistentDataPath}/config.cfg");

            //optionsData = JsonUtility.FromJson<OptionsData>(json);

            var bf = new BinaryFormatter();

            FileStream file = File.Open($"{Application.persistentDataPath}/config.cfg", FileMode.Open);

            optionsData = (OptionsData)bf.Deserialize(file);
            file.Close();
        }
    }
    [Serializable]
    public class OptionsData {
        [SerializeField] private string nickName = "Guestt";
    }
}
