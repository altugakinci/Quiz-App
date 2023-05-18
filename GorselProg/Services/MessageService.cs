using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GorselProg.Model;
using GorselProg.Session;

namespace GorselProg.Services
{
    public static class MessageService
    {

        public static async Task SendMessageAsync(Guid userId, string messageText, Guid roomId)
        {
            using (var context = new qAppDBContext())
            {
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    MessageText = messageText,
                    SentTime = DateTime.Now,
                    UserId = userId,
                    RoomId = roomId
                };

                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }

        }

        public static async Task<List<Message>> GetMessagesByUserId(Guid userId, Guid roomId)
        {
            try
            {
                
                using (var context = new qAppDBContext())
                {
                    var messages=await context.Messages
                        .Where(m=>m.RoomId==roomId)
                        .Where(m=>m.UserId==userId)
                        .ToListAsync();
                    return messages;
                }
            }
            finally
            {

            }
            
            
        }

        public static async Task<List<Message>> GetMessagesByRoomId(Guid roomId)
        {
            return await _dbContext.Messages
                .Where(m => m.RoomId == roomId)
                .ToListAsync();
        }

        public static async Task<Message> GetMessageById(Guid messageId)
        {
            return await _dbContext.Messages
                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public static async Task SendMessageAndSaveAsync(Guid userId, string messageText, Guid roomId)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageText = messageText,
                SentTime = DateTime.Now,
                UserId = userId,
                RoomId = roomId
            };

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }

        public static async Task SendMessageAndSaveAsync(Guid userId, string messageText, Room room)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageText = messageText,
                SentTime = DateTime.Now,
                UserId = userId,
                RoomId = room.Id,
                Room = room
            };

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}

