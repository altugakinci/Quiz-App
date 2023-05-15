using GorselProg.Model;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GorselProg.Services
{


    static class RoomService
    {
        public static bool loadingIndicator = false;


        public static void ShowLoadingIndicator()
        {
            loadingIndicator = true;
        }
        public static void HideLoadingIndicator()
        {
            loadingIndicator = true;
        }

        public static async Task<bool> CreateRoom(Room room)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
             

                
                    var newRoom = new Room
                    {
                        Id = Guid.NewGuid(),
                        Name = room.Name,
                        Password = room.Password,
                        Code = room.Code,
                        AdminId = room.AdminId,
                    };

                    db.Rooms.Add(newRoom);
                    await db.SaveChangesAsync();


                }
                return true;
            }
            //catch
            //{
            //    return false;
            //}
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<List<Room>> ListRooms()
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var rooms = await context.Rooms.ToListAsync();
                    return rooms;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> JoinRoom(Guid userId, Guid roomId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                    if (room != null && user != null)
                    {
                        var player = new Player
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            RoomId = room.Id
                        };
                        room.Players.Add(player);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> BanUser(Guid roomId, Guid userId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                    if (room != null && user != null)
                    {
                        var bannedUser = new BannedUser
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            RoomId = room.Id
                        };
                        room.BannedUsers.Add(bannedUser);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<List<User>> GetRoomUsers(Guid roomId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var users = await db.Players
                    .Where(p => p.RoomId == roomId)
                    .Select(p => p.User)
                    .ToListAsync();

                    return users;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }
    }
}

