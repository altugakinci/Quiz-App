using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Services
{

    class QuestionService
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
        public static async Task<List<Question>> GetAllQuestions()
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var questions = await db.Questions.Include(q => q.Category).ToListAsync(); ;
                    return questions;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<Question> GetQuestionById(Guid id)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var question = await db.Questions.FirstOrDefaultAsync(q => q.Id == id);
                    return question;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> AddQuestion(Question question)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    db.Questions.Add(question);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            /*catch
            {
                return false;
            }*/
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> UpdateQuestion(Question question)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    db.Entry(question).State = EntityState.Modified;
                    await db.SaveChangesAsync();
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

        public static async Task<bool> DeleteQuestion(Guid id)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var question = await db.Questions.FirstOrDefaultAsync(q => q.Id == id);
                    if (question != null)
                    {
                        db.Questions.Remove(question);
                        await db.SaveChangesAsync();
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

        public static async Task<List<Question>> GetQuestionsByCategory(Guid categoryId)
        {
            try
            {
                ShowLoadingIndicator();
                using (var db = new qAppDBContext())
                {
                    var questions = await db.Questions
                        .Where(q => q.CategoryId == categoryId)
                        .ToListAsync();
                    return questions;
                }
            }
            finally
            {
                HideLoadingIndicator();
            }
        }
    }
}

