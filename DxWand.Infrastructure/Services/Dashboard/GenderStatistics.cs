using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Enums;
using DxWand.Core.Interfaces;
using DxWand.Core.Repositories;

namespace DxWand.Infrastructure.Services.Dashboard
{
    public class GenderStatistics : IStatistics<GenderEnum> 
    {
        private readonly IUserRepository _userRepository;
        public GenderStatistics(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Dictionary<GenderEnum, int>> BuildStatisticsModel()
        {
            var usersGender = await _userRepository.GetUsersGenderAsync();
            var dataStatistics = new Dictionary<GenderEnum, int>();
            #region Generate Gender Statistics
            dataStatistics[GenderEnum.Male] = 0;
            dataStatistics[GenderEnum.Female] = 0;
            dataStatistics[GenderEnum.Other] = 0;
            foreach (var gender in usersGender) 
            {
                if (gender == GenderEnum.Male || gender == GenderEnum.Female || gender == GenderEnum.Other)
                    dataStatistics[gender]++;
            }
            #endregion
            return dataStatistics;
        }
    }
}
