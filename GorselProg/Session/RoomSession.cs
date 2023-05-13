using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Session
{
    class RoomSession
    {
        private static RoomSession _instance;
        private List<Category> _allCategories;
        private List<Category> _selectedCategories;
        private Room _currentRoom;

        private RoomSession()
        {
            _allCategories = new List<Category>();
            _selectedCategories = new List<Category>();
            _currentRoom = new Room();
        }

        public static RoomSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoomSession();
                }
                return _instance;
            }
        }

        // DB den çekilen kategorileri set etmek
        public void SetAllCategories(List<Category> categories)
        {
            _allCategories = categories;
        }

        public void AddSelectedCategory(Category category)
        {
            _selectedCategories.Add(category);
        }

        public void RemoveSelectedCategory(Category category)
        {
            _selectedCategories.Remove(category);
        }

        public List<Category> GetAllCategories()
        {
            return _allCategories;
        }

        public List<Category> GetSelectedCategories()
        {
            return _selectedCategories;
        }
        public void SetCurrentRoom(Room room)
        {
            _currentRoom = room;
        }
        public Room GetCurrentRoom()
        {
            return _currentRoom;
        }
    }
}
