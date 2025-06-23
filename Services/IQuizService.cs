using Quiz.Models;
namespace Quiz.Services
{
    public interface IQuizService
    {
        Task<List<IGrouping<QuestionGroupKey, QuizEntry>>> GetAllQuestionsGrouped();
        Task<List<QuizEntry>> GetQuestionGroup(int questionId);

        Task AddQuestionGroup(string questionText, List<(string AnswerText, bool IsCorrect)> answers);
        Task UpdateQuestionGroup(string oldQuestionText, string newQuestionText, List<(string AnswerText, bool IsCorrect)> answers);

        Task<QuizEntry?> GetCorrectAnswer(string questionText);


    }
}

