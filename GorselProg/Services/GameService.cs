using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Services
{
    public static class GameService
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
        public static async Task<Game> StartGame(Guid roomId, List<Category> categories, DateTime startTime,  DateTime endTime )
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

                    if (room != null)
                    {
                        var newGame = new Game
                        {
                            Id = Guid.NewGuid(),
                            StartTime = startTime,
                            EndTime = endTime,
                            RoomId = roomId
                        };

                        // Kategorilere ait rastgele 10 soru getirme
                        var categoryIds = categories.Select(c => c.Id).ToList();
                        var randomQuestions = await context.Questions
                            .Where(q => categoryIds.Contains((Guid)q.CategoryId))
                            .OrderBy(q => Guid.NewGuid())
                            .Take(2)
                            .ToListAsync();


                        foreach (var question in randomQuestions)
                        {
                            var gameQuestion = new GameQuestion
                            {
                                Id = Guid.NewGuid(),
                                GameId = newGame.Id,
                                QuestionId = question.Id
                            };

                            newGame.GameQuestions.Add(gameQuestion);
                        }

                        room.CurrentGameId = newGame.Id;

                        context.Games.Add(newGame);
                        await context.SaveChangesAsync();

                        return newGame;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }
    }
}
