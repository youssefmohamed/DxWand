using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Enums;

namespace DxWand.Application.Dashboard.Responses
{
    public class DashboardResponse
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
