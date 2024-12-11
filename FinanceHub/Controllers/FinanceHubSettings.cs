using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHub.Controllers
{

    public class FinanceHubSettings
    {
        public int CurrentTab { get; set; }
        public string? CurrentUser { get; set; }
        public string[] Users { get; set; }

        public FinanceHubSettings()
        {
            Users = [];
        }

    }
}
