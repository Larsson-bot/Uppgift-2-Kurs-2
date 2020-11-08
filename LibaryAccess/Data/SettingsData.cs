using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LibaryAccess.Data
{
    public static class SettingsData
    {
        private static Settings _settings { get; set; }
        public static async Task GetSettingsFromFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            await storageFolder.CreateFileAsync("Settings.json", CreationCollisionOption.OpenIfExists);
            StorageFile file = await storageFolder.GetFileAsync("Settings.json");
            await FileIO.WriteTextAsync(file, "{\"Status\": [\"new\",\"active\",\"completed\"],\"MaxActiveErrands\": 5 }");
            _settings = JsonConvert.DeserializeObject<Settings>(await FileIO.ReadTextAsync(file));
        }
        public static IEnumerable<string> GetStatus()
        {
            var list = new List<string>();
            foreach (var status in _settings.Status)
            {
                list.Add(status);
            }
            return list;
        }
        public static int GetMaxActiveValue()
        {
            return _settings.MaxActiveErrands;
        }
    }



    public class Settings
    {
        public string[] Status { get; set; }
        public int MaxActiveErrands { get; set; }
    }


}
