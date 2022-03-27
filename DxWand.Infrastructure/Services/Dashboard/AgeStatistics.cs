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
    public class AgeStatistics : IStatistics<AgeStatisticsEnum>
    {
        private readonly IUserRepository _userRepository;
        public AgeStatistics(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Dictionary<AgeStatisticsEnum, int>> BuildStatisticsModel()
        {
            var usersBirthDate = await _userRepository.GetUsersBirthdateAsync();
            var dataStatistics = new Dictionary<AgeStatisticsEnum, int>();
            #region Generate Age Statistics
            var ageStatisticsEnumValues = Enum.GetValues(typeof(AgeStatisticsEnum));
            foreach (var ageStatisticsEnum in ageStatisticsEnumValues)
                dataStatistics[(AgeStatisticsEnum)ageStatisticsEnum] = 0;
            
            foreach (var birthdate in usersBirthDate)
            {
                var age = CalculateAge(birthdate);
                if (age <= 10)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual10]++;
                else if (age > 10 && age <= 20)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual20]++;
                else if (age > 20 && age <= 30)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual30]++;
                else if (age > 30 && age <= 40)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual40]++;
                else if (age > 40 && age <= 50)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual50]++;
                else if (age > 50 && age <= 60)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual60]++;
                else if (age > 60 && age <= 70)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual70]++;
                else if (age > 70 && age <= 80)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual80]++;
                else if (age > 80 && age <= 90)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual90]++;
                else if (age > 90 && age <= 100)
                    dataStatistics[AgeStatisticsEnum.LessThanOrEqual100]++;

            }
            #endregion
            return dataStatistics;
        }

        private int CalculateAge(DateTime birthdate) 
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (birthdate.Year * 100 + birthdate.Month) * 100 + birthdate.Day;
            return (a - b) / 10000;
        }
    }
}
