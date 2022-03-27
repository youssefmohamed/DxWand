using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxWand.Core.Interfaces
{
    public interface IStatistics<T>
    {
        Task<Dictionary<T, int>> BuildStatisticsModel();
    }
}
