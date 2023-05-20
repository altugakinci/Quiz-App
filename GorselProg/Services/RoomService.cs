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
        public static bool loadingIndicator;
        public static void ShowLoadingIndicator()
        {
            loadingIndicator = true;
        }

        public static void HideLoadingIndicator()
        {
            loadingIndicator = false;
        }

        public static async Task<bool> CreateRoom(Room newRoom)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                 

                    context.Rooms.Add(newRoom);
                    await context.SaveChangesAsync();

                    RoomSession.Instance.SetCurrentRoom(newRoom);
                    // set all categories to session
                    var categories = await context.Categories.ToListAsync();
                    RoomSession.Instance.SetAllCategories(categories);
                    var player = new Player
                    {
                        Id = Guid.NewGuid(),
                        RoomId = newRoom.Id,
                        UserId = newRoom.AdminId
                    };

                    context.Players.Add(player);
                    await context.SaveChangesAsync();
                    
                    return true;
                }
            }
            catch
            {
                return false;
            }
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
        public static async Task<List<User>> GetPlayers(Guid roomId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var players = await context.Players
                        .Where(p => p.RoomId == roomId)
                        .Select(p => p.User)
                        .ToListAsync();

                    return players;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> JoinRoom(string code, string password, User user)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var room = await context.Rooms.FirstOrDefaultAsync(r => r.Code == code && r.Password == password);

                    if (room != null)
                    {
                        RoomSession.Instance.SetCurrentRoom(room);
                        var existingPlayer = await context.Players.FirstOrDefaultAsync(p => p.RoomId == room.Id && p.UserId == user.Id);

                        if (existingPlayer == null)
                        {
                            var player = new Player
                            {
                                Id = Guid.NewGuid(),
                                RoomId = room.Id,
                                UserId = user.Id
                            };

                            context.Players.Add(player);
                            await context.SaveChangesAsync();
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> ExitRoom(Guid roomId, User user)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var player = await context.Players.FirstOrDefaultAsync(p => p.RoomId == roomId && p.UserId == user.Id);

                    if (player != null)
                    {
                        bool isPlayerAdmin = player.UserId == (await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId)).AdminId; // Oyuncu admin mi kontrol ediyoruz

                        context.Players.Remove(player);
                        await context.SaveChangesAsync();

                        // Admin ise adminliği devret
                        if (isPlayerAdmin)
                        {
                            var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                            var remainingPlayers = await context.Players.Where(p => p.Id != player.Id).ToListAsync();

                            if (remainingPlayers.Count > 0)
                            {
                                Random random = new Random();
                                int randomIndex = random.Next(0, remainingPlayers.Count);
                                var newAdminPlayer = remainingPlayers[randomIndex];
                                room.AdminId = newAdminPlayer.UserId; // Yeni adminin UserId'sini room.AdminId'ye atıyoruz
                            }
                        }

                        RoomSession.Instance.SetCurrentRoom(null);
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> BanUser(Guid userId, Guid roomId, Guid adminId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.AdminId == adminId);

                    if (room != null)
                    {
                        var player = await context.Players.FirstOrDefaultAsync(p => p.UserId == userId && p.RoomId == roomId);

                        if (player != null)
                        {
                            var bannedUser = new BannedUser
                            {
                                Id = Guid.NewGuid(),
                                UserId = player.UserId,
                                RoomId = player.RoomId
                            };

                            context.Players.Remove(player);
                            context.BannedUsers.Add(bannedUser);

                            await context.SaveChangesAsync();
                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> KickUser(Guid roomId, Guid userId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var player = await context.Players.FirstOrDefaultAsync(p => p.RoomId == roomId && p.UserId == userId);

                    if (player != null)
                    {
                        context.Players.Remove(player);
                        await context.SaveChangesAsync();
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }
    }
}

