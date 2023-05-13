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
        private readonly qAppDBContext _context;

        public QuestionService(qAppDBContext context)
        {
            _context = context;
        }

        public async Task AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestion(Question question)
        {
            var existingQuestion = await _context.Questions.FindAsync(question.Id);
            if (existingQuestion != null)
            {
                _context.Entry(existingQuestion).CurrentValues.SetValues(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByCategories(List<Category> categories)
        {
            var questionIds = await _context.Questions
                .Where(q => categories.Any(c => q.Categories.Contains(c)))
                .Select(q => q.Id)
                .ToListAsync();

            var questions = await _context.Questions
                .Where(q => questionIds.Contains(q.Id))
                .ToListAsync();

            return questions;
        }
    }
}

