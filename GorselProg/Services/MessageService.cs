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
                        .Where(m=>m.RoomId==roomId && m.UserId==userId)
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
            try
            {
                using (var context = new qAppDBContext())
                {
                    var messages = await context.Messages
                        .Where(m => m.RoomId == roomId)
                        .ToListAsync();
                    return messages;
                }
            }
            finally
            {

            }
        }

        public static async Task<Message> GetMessageById(Guid messageId)
        {
            try
            {
                using (var context = new qAppDBContext())
                {
                    var message = await context.Messages
                        .Where(m => m.Id == messageId)
                        .FirstOrDefaultAsync();
                    return message;
                }
            }
            finally
            {

            }
        }
    }
}

