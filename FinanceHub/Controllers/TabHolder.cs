using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace FinanceHub.Controllers
{
    internal class TabHolder
    {
        readonly IFileSystem fileSystem;




        public TabHolder(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        private string _fileName = "settings.json";

        public enum TabOptions  {Input, Data}

        public FinanceHubSettings GetStartingTab()
        {
            

            if (!this.fileSystem.File.Exists(_fileName))
            {
               FinanceHubSettings settings = new FinanceHubSettings { CurrentTab= (int)TabHolder.TabOptions.Input};
                WriteSettings(settings);
            }

            FinanceHubSettings SettingsFromFile = ReadSettings();

            return SettingsFromFile;
        }
        private FinanceHubSettings ReadSettings()
        {
            var SettingsFromFile =  JsonSerializer.Deserialize<FinanceHubSettings>(this.fileSystem.File.ReadAllText(_fileName));
            return SettingsFromFile == null
          ? throw new ArgumentNullException("settings.json was damaged somehow")
          : SettingsFromFile;
        }

        private void WriteSettings(FinanceHubSettings Settings)
        {
            string jsonString = JsonSerializer.Serialize(Settings);
            this.fileSystem.File.WriteAllText(_fileName, jsonString);
        }

        public void SaveTab(int tab)
        {
            FinanceHubSettings?  SettingsFromFile = ReadSettings();
            SettingsFromFile.CurrentTab = tab;
            WriteSettings(SettingsFromFile);
        }

    }

    public class FinanceHubSettings
    {
        public int CurrentTab { get; set; }
    }

}
