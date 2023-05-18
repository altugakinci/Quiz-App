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

        //public static async Task<bool> CreateRoom(Room room)
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var db = new qAppDBContext())
        //        {
             

                
        //            var newRoom = new Room
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = room.Name,
        //                Password = room.Password,
        //                Code = room.Code,
        //                AdminId = room.AdminId,
        //            };

        //            db.Rooms.Add(newRoom);
        //            await db.SaveChangesAsync();


        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}

        //public static async Task<List<Room>> ListRooms()
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var context = new qAppDBContext())
        //        {
        //            var rooms = await context.Rooms.ToListAsync();
        //            return rooms;
        //        }
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}

        //public static async Task<bool> JoinRoom(string code, string password, User user)
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var db = new qAppDBContext())
        //        {
        //            var room = await db.Rooms.FirstOrDefaultAsync(r => r.Code == code);

        //            if (room == null)
        //            {
        //                // Belirtilen kodla eşleşen bir oda bulunamadı
        //                return false;
        //            }

        //            if (room.Password != password)
        //            {
        //                // Şifre eşleşmiyor
        //                return false;
        //            }

        //            var existingPlayer = await db.Players.FirstOrDefaultAsync(p => p.RoomId == room.Id && p.UserId == user.Id);

        //            if (existingPlayer != null)
        //            {
        //                // Kullanıcı zaten bu odaya daha önce giriş yapmış
        //                return true;
        //            }

        //            var player = new Player
        //            {
        //                Id = Guid.NewGuid(),
        //                RoomId = room.Id,
        //                UserId = user.Id
        //            };

        //            db.Players.Add(player);
        //            await db.SaveChangesAsync();

        //            room.PlayersId = player.Id; // PlayersId alanını güncelle

        //            await db.SaveChangesAsync();

        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}

        //public static async Task<bool> ExitRoom(Guid roomId, User user)
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var db = new qAppDBContext())
        //        {
        //            var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

        //            if (room == null)
        //            {
        //                // Belirtilen roomId ile eşleşen bir oda bulunamadı
        //                return false;
        //            }

        //            var player = room.Players.FirstOrDefault(p => p.UserId == user.Id);

        //            if (player == null)
        //            {
        //                // Kullanıcının odaya katılımı bulunmamaktadır
        //                return false;
        //            }

        //            room.Players.Remove(player); // Players listesinden kullanıcıyı sil

        //            await db.SaveChangesAsync();

        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}

        //public static async Task<bool> BanUser(Guid roomId, Guid userId)
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var db = new qAppDBContext())
        //        {
        //            var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        //            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

        //            if (room != null && user != null)
        //            {
        //                var bannedUser = new BannedUser
        //                {
        //                    Id = Guid.NewGuid(),
        //                    UserId = user.Id,
        //                    RoomId = room.Id
        //                };
        //                room.BannedUsers.Add(bannedUser);
        //                room.BannedUsersId = bannedUser.Id;
        //                await db.SaveChangesAsync();
        //                return true;
        //            }
        //            return false;
        //        }
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}

        //public static async Task<List<User>> GetRoomUsers(Guid roomId)
        //{
        //    try
        //    {
        //        ShowLoadingIndicator();
        //        using (var db = new qAppDBContext())
        //        {
        //            var users = await db.Players
        //            .Where(p => p.RoomId == roomId)
        //            .Select(p => p.User)
        //            .ToListAsync();

        //            return users;
        //        }
        //    }
        //    finally
        //    {
        //        HideLoadingIndicator();
        //    }
        //}
    }
}

