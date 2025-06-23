using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;

namespace Quiz.Services
{
    public class QuizService: IQuizService
    {
        private readonly AppDbContext _appDbContext;

        public QuizService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }
        public async Task<List<IGrouping<QuestionGroupKey, QuizEntry>>> GetAllQuestionsGrouped()
        {
            return await _appDbContext.QuizEntries
                .AsNoTracking()
                .GroupBy(q => new QuestionGroupKey(q.QuestionId, q.QuestionText))
                .ToListAsync();
        }



        public async Task<List<QuizEntry>> GetQuestionGroup(int questionId)
        {
            return await _appDbContext.QuizEntries
                        .Where(q => q.QuestionId == questionId)
                        .ToListAsync();
        }


        public async Task AddQuestionGroup(string questionText, List<(string AnswerText, bool IsCorrect)> answers)
        {
            int maxId = await _appDbContext.QuizEntries.AnyAsync()
            ? await _appDbContext.QuizEntries.MaxAsync(q => q.QuestionId)
            : 0;

            int newQuestionId = maxId + 1;
            var entries = answers.Select(a => new QuizEntry
            {
                QuestionId = newQuestionId,
                QuestionText = questionText,
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect
            }).ToList();

            _appDbContext.QuizEntries.AddRange(entries);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateQuestionGroup(string oldQuestionText, string newQuestionText, List<(string AnswerText, bool IsCorrect)> answers)
        {
            var existing = await _appDbContext.QuizEntries
                                    .Where(q => q.QuestionText == oldQuestionText)
                                    .ToListAsync();
            int questionId = existing.First().QuestionId;
            _appDbContext.QuizEntries.RemoveRange(existing);

            var updated = answers.Select(a => new QuizEntry
            {
                QuestionId = questionId,
                QuestionText = newQuestionText,
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect
            });

            _appDbContext.QuizEntries.AddRange(updated);
            await _appDbContext.SaveChangesAsync();
        }


        public async Task<QuizEntry?> GetCorrectAnswer(string questionText)
        {
            return await _appDbContext.QuizEntries
                        .FirstOrDefaultAsync(q => q.QuestionText == questionText && q.IsCorrect);
        }






    }
}
