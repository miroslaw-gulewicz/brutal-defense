
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static PlayerProgressData;

namespace Utilities
{
    internal class SaveSystem
    {
        private static string SAVE_FILENAME = "brutal.def";

        private static string SAVE_PATH = Application.persistentDataPath + "/" + SAVE_FILENAME;

        public static void SavePlayer(PlayerProgressData playerData)
        {
            DoSerialize(playerData);
        }

        public static PlayerProgressData.SaveData LoadPlayerData()
        {
            if (!IsSaveGame())
            {
                Debug.LogError("No savefile " + SAVE_PATH);
                return new SaveData();
            }

            FileStream dataStream = new FileStream(SAVE_PATH, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            dataStream.Position = 0;
            object data = converter.Deserialize(dataStream);

            dataStream.Close();
            return (SaveData)data;

        }

        public static void DoSerialize(PlayerProgressData progressData)
        {

            FileStream stream = new FileStream(SAVE_PATH, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(stream, new SaveData
            {
                _killCount = progressData.KillCount,
                _playTime = progressData.PlayTime,
            });

            stream.Close();

        }

        internal static bool IsSaveGame()
        {
            return File.Exists(SAVE_PATH);
        }
    }

}