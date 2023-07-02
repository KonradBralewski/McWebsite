using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerReport.Enums
{
    public enum ReportType
    {
        ServerCrash,
        LaggingPerformance,
        Downtime,
        DataLoss,
        UnauthorizedAccess,
        ExploitableVulnerability,
        InappropriateContent,
        ServerMisconfiguration,
        UnreliableConnectivity,
        Other
    }

}
