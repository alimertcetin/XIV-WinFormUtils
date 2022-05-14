using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace XIV.SaveSystems
{

    public static class SaveSystem
    {
        public static void Save(ISaveable saveable, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    object saveData = saveable.GetSaveData();
                    bf.Serialize(fs, saveData);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public static void Load<TSaveable>(ref TSaveable saveable, string path) 
            where TSaveable : ISaveable
        {
            saveable = (TSaveable)Activator.CreateInstance(typeof(TSaveable));
            if (!File.Exists(path))
            {
                return;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    saveable.Load(bf.Deserialize(fs));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}