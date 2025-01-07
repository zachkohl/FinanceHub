using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FinanceHub.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinanceHub.Services
{
   public abstract class RememberStateBase
    {
        internal string _fileName = "settings.json";

        internal readonly IFileSystem _fileSystem;

        

        public RememberStateBase(IFileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
       
        }

        internal FinanceHubSettings ReadSettings()
        {
            if (!_fileSystem.File.Exists(_fileName))
            {
                FinanceHubSettings settings = new FinanceHubSettings { CurrentTab = 0 };
                WriteSettings(settings);
            }

            var SettingsFromFile = JsonSerializer.Deserialize<FinanceHubSettings>(_fileSystem.File.ReadAllText(_fileName));
            return SettingsFromFile == null
          ? throw new ArgumentNullException("settings.json was damaged somehow")
          : SettingsFromFile;
        }

        internal void WriteSettings(FinanceHubSettings Settings)
        {
 



            string jsonString = JsonSerializer.Serialize(Settings);
            _fileSystem.File.WriteAllText(_fileName, jsonString);
        }


    }
}
