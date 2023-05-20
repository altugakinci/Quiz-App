using GorselProg.Model;
using GorselProg.Objects;
using GorselProg.Session;
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
        public static async Task<Game> StartGame(Guid roomId, List<Category> categories, DateTime startTime, DateTime endTime)
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
                        // Todo: User Player tablosunda var mı?

                        // Kategorilere ait rastgele 10 soru getirme
                       
                        var categoryIds = categories.Select(c => c.Id).ToList();
                        var randomQuestions = await context.Questions
                            .Where(q => categoryIds.Contains((Guid)q.CategoryId))
                            .OrderBy(q => Guid.NewGuid())
                            .Take(2)
                            .ToListAsync();
                        //set to session all questions
                        GameSession.Instance.SetAllQuestions(randomQuestions);
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

                        RoomSession.Instance.SetCurrentRoom(room);

                        context.Games.Add(newGame);
                        await context.SaveChangesAsync();

                        GameSession.Instance.SetCurrentGame(newGame);

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

        public static async Task<bool> AnswerQuestion(Guid userId, Guid questionId, Guid gameId, string answerText)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    var question = await context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
                    var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId);

                    string[] questionAnswers = Helper.SplitString(question.OptionsText);
                    string correctAnswer = questionAnswers[question.CorrectAnswerIndex];


                    var isCorrectAnswer = correctAnswer.Equals(answerText, StringComparison.OrdinalIgnoreCase) ? 50 : 0;

                    if (user != null && question != null && game != null)
                    {
                        var answer = new Answer
                        {
                            Id = Guid.NewGuid(),
                            AnswerText = answerText,
                            GainedXp = isCorrectAnswer,
                            UserId = userId,
                            QuestionId = questionId,
                            GameId = gameId
                        };

                        context.Answers.Add(answer);
                        await context.SaveChangesAsync();
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

        public static async Task<Question> GetQuestionsByGame(Guid gameId)
        {
            try
            {
                using (var context = new qAppDBContext())
                {
                    var gameQuestion = await context.GameQuestions
                        .Where(gq => gq.GameId == gameId)
                        .FirstOrDefaultAsync();

                    if (gameQuestion != null)
                    {
                        var question = await context.Questions
                            .FirstOrDefaultAsync(q => q.Id == gameQuestion.QuestionId);

                        return question;
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<SummaryGame> GetSummaryGame(Guid gameId,Guid userId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var summaryGame = new SummaryGame();

                    // Top 3 users with highest gained XP
                    var topUsers = await context.Answers
                        .Where(a => a.GameId == gameId)
                        .GroupBy(a => a.UserId)
                        .Select(g => new { UserId = g.Key, SumXP = g.Sum(a => a.GainedXp) })
                        .OrderByDescending(g => g.SumXP)
                        .Take(3)
                        .ToListAsync();

                    if (topUsers.Count >= 1)
                    {
                        Guid? firstUser = topUsers[0].UserId;
                        summaryGame.FirstUser = await context.Users.FirstOrDefaultAsync(u => u.Id == firstUser);
                    }
                    if (topUsers.Count >= 2)
                    {
                        Guid? secondUser = topUsers[1].UserId;
                        summaryGame.SecondUser = await context.Users.FirstOrDefaultAsync(u => u.Id == secondUser);
                    }
                        
                    if (topUsers.Count >= 3)
                    {
                        Guid? thirdUser = topUsers[2].UserId;
                        summaryGame.ThirdUser = await context.Users.FirstOrDefaultAsync(u => u.Id == thirdUser);
                    }
                        

                    // Category-wise correct answers for a specific user
                    var categoryIds = await context.Categories.Select(c => c.Id).ToListAsync();

                    Guid? Category_1 = categoryIds[0];
                    Guid? Category_2 = categoryIds[1];
                    Guid? Category_3 = categoryIds[2];
                    Guid? Category_4 = categoryIds[3];
                    Guid? Category_5 = categoryIds[4];


                    summaryGame.Category1Correct = await context.Answers
                        .CountAsync(a => a.UserId == userId && a.GameId == gameId && a.Question.CategoryId == Category_1 && a.GainedXp > 0);
                    summaryGame.Category2Correct = await context.Answers
                        .CountAsync(a => a.UserId == userId && a.GameId == gameId && a.Question.CategoryId == Category_2 && a.GainedXp > 0);
                    summaryGame.Category3Correct = await context.Answers
                        .CountAsync(a => a.UserId == userId && a.GameId == gameId && a.Question.CategoryId == Category_3 && a.GainedXp > 0);
                    summaryGame.Category4Correct = await context.Answers
                        .CountAsync(a => a.UserId == userId && a.GameId == gameId && a.Question.CategoryId == Category_4 && a.GainedXp > 0);
                    summaryGame.Category5Correct = await context.Answers
                        .CountAsync(a => a.UserId == userId && a.GameId == gameId && a.Question.CategoryId == Category_5 && a.GainedXp > 0);

                    // Sum of gained XP for a specific user
                    summaryGame.SumXP = await context.Answers
                        .Where(a => a.UserId == userId && a.GameId == gameId)
                        .SumAsync(a => a.GainedXp);

                    // User's level
                   
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user != null)
                    {
                        int result = (summaryGame.SumXP / 500);
                        if (result > 0)                      
                            summaryGame.isLevelUp = true;
                        else
                            summaryGame.isLevelUp = false;
                        
                        user.Level = user.Level + result;
                        user.Xp = (summaryGame.SumXP % 500);

                        summaryGame.Level = user.Level ;
                        summaryGame.SumXP = user.Xp;

                    }

                    return summaryGame;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> EndGame(Guid gameId, Guid roomId)
        {
            try
            {
                ShowLoadingIndicator();

                using (var context = new qAppDBContext())
                {
                    var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId && g.RoomId == roomId);

                    if (game == null)
                    {
                        // Oyun bulunamadı
                        return false;
                    }

                    // Oyunun başlangıç ve bitiş tarihlerini kontrol et
                    DateTime now = DateTime.Now;
                    if (now < game.StartTime || now > game.EndTime)
                    {
                        // Oyun süresi dışında, oyunu bitiremezsiniz
                        return false;
                    }

                    // Oyunun bitiş tarihini güncelle
                    game.EndTime = now;

                    // Room'un currentGameId'sini null yap
                    var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                    if (room != null)
                    {
                        room.CurrentGameId = null;
                    }
                    RoomSession.Instance.SetCurrentRoom(room);
                    GameSession.Instance.SetCurrentGame(null);
                    GameSession.Instance.SetAllQuestions(null);
                    await context.SaveChangesAsync();
                }

                return true;
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
