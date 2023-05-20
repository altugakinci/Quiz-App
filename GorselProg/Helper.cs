using GorselProg.Model;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProg
{
    public static class Helper
    {
        public static string ConcatenateStrings(params string[] strings)
        {
            return string.Join("\n", strings);
        }

        public static string[] SplitString(string input)
        {
            return input.Split('\n');
        }

        public static int FindFirstTrueIndex(bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i])
                {
                    return i;
                }
            }

            return -1; // Eğer true eleman bulunamazsa -1 döndürülür
        }

        public static void AddSelectedCategory(int buttonIndex)
        {
            List<Category> allCategories = RoomSession.Instance.GetAllCategories();
            
            Category selectedCategory = allCategories.FirstOrDefault(c => c.Index == buttonIndex);

            RoomSession.Instance.AddSelectedCategory(selectedCategory);
        }

        public static void RemoveSelectedCategory(int buttonIndex)
        {
            List<Category> allCategories = RoomSession.Instance.GetAllCategories();
            Category selectedCategory = allCategories.FirstOrDefault(c => c.Index == buttonIndex);

            RoomSession.Instance.RemoveSelectedCategory(selectedCategory);
        }

        public static void toggleButtons(object sender, int buttonIndex)
        {

            Button button = (Button)sender;

            if (button.ForeColor != Color.Green)
            {
                button.ForeColor = Color.Green;
                AddSelectedCategory(buttonIndex);
            }
            else
            {
                button.ForeColor = ThemeHandler.color_texts;
                RemoveSelectedCategory(buttonIndex);
            }

        }

        public static string GenerateRoomCode()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            char randomChar = characters[random.Next(characters.Length)];
            string randomString = new string(
                Enumerable.Repeat(characters, 3)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray()
            );

            return randomChar + randomString;
        }
    }
}
