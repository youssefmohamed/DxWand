using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Entities;
using DxWand.Core.Repositories;
using DxWand.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DxWand.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Message> AddAsync(Message message)
        {
            await _applicationDbContext.Set<Message>().AddAsync(message);
            await _applicationDbContext.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await _applicationDbContext.Messages.ToListAsync();
        }

        public async Task<List<Message>> GetByUserIdAsync(string userId)
        {
            return await _applicationDbContext
                        .Messages.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
