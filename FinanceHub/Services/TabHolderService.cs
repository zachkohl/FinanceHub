using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FinanceHub.DataBase;
using Microsoft.VisualBasic;

namespace FinanceHub.Services
{
    internal class TabHolderService: RememberStateBase
    {
      
        public TabHolderService(IFileSystem fileSystem) : base(fileSystem)
        { }   
        
        public enum TabOptions  {Input, Data}

        public FinanceHubSettings GetStartingTab()
        {

            FinanceHubSettings SettingsFromFile = ReadSettings();

            return SettingsFromFile;
        }
       

        public void SaveTab(int tab)
        {
            FinanceHubSettings?  SettingsFromFile = ReadSettings();
            SettingsFromFile.CurrentTab = tab;
            WriteSettings(SettingsFromFile);
        }

    }


}
