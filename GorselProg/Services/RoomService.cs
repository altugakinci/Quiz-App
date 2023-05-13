using GorselProg.Model;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GorselProg.Services
{


    class RoomService
    {
        private readonly qAppDBContext _context;

        public RoomService(qAppDBContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoom(string name, string password, User admin)
        {
            List<Category> allCategories = await _context.Categories.ToListAsync();
            RoomSession.Instance.SetAllCategories(allCategories);

            var room = new Room
            {
                Name = name,
                Password = password,
                Admin = admin,
                Users = new List<User>()
            };
            

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            RoomSession.Instance.SetCurrentRoom(room);

            return room;
        }

        public async Task<List<Room>> ListRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var result = rooms.Where(r => r.Admin != null).ToList();
            return result;
        }

        public async Task<bool> EnterRoom(string name, string password, User user)
        {
            var room = await _context.Rooms.Include(r => r.Users).FirstOrDefaultAsync(r => r.Name == name && r.Password == password);

            if (room == null)
            {
                return false;
            }

            if (room.BannedUsers != null && room.BannedUsers.Contains(user))
            {
                return false;
            }

            room.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        // TODO:BannedUser ayarlanacak
        public async Task<bool> BanUser(int roomId, int userId, User admin)
        {
            var room = await _context.Rooms.Include(r => r.Admin).Include(r => r.BannedUsers).FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null || room.Admin.Id != admin.Id)
            {
                return false;
            }

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return false;
            }

            if (room.BannedUsers == null)
            {
                room.BannedUsers = new List<User>();
            }

            room.BannedUsers.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetRoomUsers(int roomId)
        {
            var room = await _context.Rooms.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                return null;
            }

            return room.Users.ToList();
        }

        public async Task<bool> StartGame(int roomId, List<Category> categories, int adminId, DateTime endTime)
        {
            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null || room.Admin.Id != adminId)
            {
                return false;
            }

            var questions = await _context.Questions
                .Where(q => categories.Any(c => q.Categories.Contains(c)))
                .OrderBy(q => Guid.NewGuid())
                .ToListAsync();

            if (questions.Count == 0)
            {
                return false;
            }

            var game = new Game
            {
                RoomId = roomId,
                Questions = questions,
                StartTime = DateTime.Now,
                EndTime = endTime
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            room.CurrentGame = game;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExitGame(int roomId, User user)
        {
            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                return false;
            }

            room.Users.Remove(user);

            if (room.Admin.Id == user.Id)
            {
                room.Admin = null;
            }

            await _context.SaveChangesAsync();

            return true;
        }


    }
}

