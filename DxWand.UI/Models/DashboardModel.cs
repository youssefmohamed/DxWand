using System.Collections.Generic;
using DxWand.UI.Enums;

namespace DxWand.UI.Models
{
    public class DashboardModel
    {
        public Dictionary<string, int> MessageStatistics
        {
            get;
            set;
        }
        public Dictionary<GenderEnum, int> GenderStatistics
        {
            get;
            set;
        }
        public Dictionary<AgeStatisticsEnum, int> AgeStatistics
        {
            get;
            set;
        }
    }
}
