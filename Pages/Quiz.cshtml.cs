using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz.Models;
using Quiz.Services;
namespace Quiz.Pages
{
    public class QuizModel : PageModel
    {

        private readonly IQuizService _questionService;
        public QuizModel(IQuizService questionService)
        {
            _questionService = questionService;
        }

        public List<IGrouping<QuestionGroupKey, QuizEntry>> Questions { get; private set; }
        [BindProperty]
        public List<UserAnswer> UserAnswers { get; set; }
        public async Task OnGet()
        {
            Questions = await _questionService.GetAllQuestionsGrouped();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Questions = await _questionService.GetAllQuestionsGrouped();
                return Page();
            }
            Questions = await _questionService.GetAllQuestionsGrouped();
            var score = 0;
            foreach (var answer in UserAnswers)
            {
                var correctAnswer = await _questionService.GetCorrectAnswer(answer.QuestionText);
                if (correctAnswer == null)
                {
                    ModelState.AddModelError(string.Empty, $"No correct answer found for question: {answer.QuestionText}");
                    return Page();
                }
                if (correctAnswer.AnswerText != answer.AnswerText)
                {
                    ModelState.AddModelError(string.Empty, $"Incorrect answer for question: {answer.QuestionText}. Correct answer is: {correctAnswer.AnswerText}");
                }
                else
                {
                    score++;
                }
            }
            var total = UserAnswers.Count;
            var percentage = ((double)score / total) * 100;
            return RedirectToPage("/Score", new { score = percentage });
        }
    }
    public class UserAnswer
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
