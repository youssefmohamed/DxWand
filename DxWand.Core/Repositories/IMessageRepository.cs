using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Entities;

namespace DxWand.Core.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> AddAsync(Message message);
        Task<List<Message>> GetAllAsync();
        Task<List<Message>> GetByUserIdAsync(string userId);
    }
}
