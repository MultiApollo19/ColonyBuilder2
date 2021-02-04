using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ColonyBuilder.SaveLoad {
    public class SavingLoadMenager : MonoBehaviour
    {
        public static SavingLoadMenager instance;
        public string SavePath => $"{Application.persistentDataPath}/game_save.save";

        private void Awake()
        {
            instance = this;
        }

        [ContextMenu("Save")]
        public void Save() {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }
        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
            print("Panie kochany");
        }

        private Dictionary<string, object> LoadFile() {
            if (!File.Exists(SavePath)) {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(SavePath, FileMode.Open)) {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state) {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        private void CaptureState(Dictionary<string,object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
                state[saveable.ID] = saveable.CaptureState();
            }
        }
        private void RestoreState(Dictionary<string, object> state) {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
                if(state.TryGetValue(saveable.ID,out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }

}
