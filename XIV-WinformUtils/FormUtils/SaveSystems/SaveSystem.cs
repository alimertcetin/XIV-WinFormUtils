using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace XIV.SaveSystems
{

    public static class SaveSystem
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };

        public static void Save(ISavable saveable, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    object saveData = saveable.GetSaveData();
                    JsonSerializer.Serialize(fs, saveData, options);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public static TSavable Load<TSavable>(string path) 
            where TSavable : ISavable
        {
            TSavable savable = (TSavable)Activator.CreateInstance(typeof(TSavable));
            Load(ref savable, path);
            return savable;
        }

        public static void Load<TSavable>(ref TSavable savable, string path)
            where TSavable : ISavable
        {
            if (!File.Exists(path)) return;

            var type = typeof(TSavable);
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    savable.Load(JsonSerializer.Deserialize(fs, type, options));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public static void CreateBackUp(string fileToBackup)
        {
            if (File.Exists(fileToBackup) == false) return;
            string backupFilePath = fileToBackup + ".bak";
            if(File.Exists(backupFilePath))
            {
                backupFilePath += DateTime.Now.ToString("ddmmyy_hhmmss");
            }
            using (FileStream fs = new FileStream(backupFilePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(File.ReadAllBytes(fileToBackup));
            }
        }

        public static void RemoveLastBackups(int backupKeepAmount, string path)
        {
            if (backupKeepAmount <= 0 || string.IsNullOrEmpty(path)) return;

            string directory = Path.GetDirectoryName(path) ?? "";
            string fileName = Path.GetFileName(path);
            string searchPattern = fileName + ".bak*";
            var backupFiles = Directory.GetFiles(directory, searchPattern)
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.CreationTime)
                .ToList();

            // Skip the most recent backups to remove the oldest ones
            var toRemove = backupFiles.Skip(backupKeepAmount);
            foreach (var file in toRemove)
            {
                try
                {
                    file.Delete();
                }
                catch { MessageBox.Show("Failed to remove backup files."); }
            }
        }

        public static int GetBackupCount(string path)
        {
            if (string.IsNullOrEmpty(path)) return 0;

            string directory = Path.GetDirectoryName(path) ?? "";
            string fileName = Path.GetFileName(path);
            string searchPattern = fileName + ".bak*";
            try
            {
                return Directory.GetFiles(directory, searchPattern).Length;
            }
            catch
            {
                return 0;
            }
        }
    }
}