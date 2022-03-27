using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Entities;
using DxWand.Core.Interfaces;
using DxWand.Core.Repositories;

namespace DxWand.Infrastructure.Services.Dashboard
{
    public class MessageStatistics : IStatistics<string>
    {
        private readonly IMessageRepository _messageRepository;
        public MessageStatistics(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Dictionary<string, int>> BuildStatisticsModel()
        {
            var messages = await _messageRepository.GetAllAsync();
            var dataStatistics = new Dictionary<string, int>();
            #region Generate Tokens
            foreach (var message in messages)
            {
                var messageTokens = message.Content.Split(' ');
                foreach (var token in messageTokens)
                {
                    if (!string.IsNullOrWhiteSpace(token.Trim()))
                    {
                        if (dataStatistics.ContainsKey(token.ToLower()))
                            dataStatistics[token.ToLower()]++;
                        else
                            dataStatistics.Add(token.ToLower(), 1);
                    }

                }
            }
            #endregion
            return dataStatistics;
        }
    }
}
