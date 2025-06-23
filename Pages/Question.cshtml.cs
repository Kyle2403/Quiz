using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz.Models;
using Quiz.Services;
using System.Threading.Tasks;
namespace Quiz.Pages
{
    public class QuestionModel : PageModel
    {
        public List<IGrouping<QuestionGroupKey, QuizEntry>> Questions { get; private set; }

        public readonly IQuizService _questionService;

        [BindProperty]
        public QuestionInputModel QuestionInputModel { get; set; }

        public QuestionModel(IQuizService questionService)
        {
            _questionService = questionService;
        }

        public async Task OnGet()
        {
            Questions = await _questionService.GetAllQuestionsGrouped();
        }

        public async Task<IActionResult> OnPost()
        {
            Questions = await _questionService.GetAllQuestionsGrouped();
            if (!ModelState.IsValid) return Page();
            
            var answers = QuestionInputModel.Answers.Select((a, index) => (a, index == QuestionInputModel.CorrectOptionIndex)).ToList();
            await _questionService.AddQuestionGroup(QuestionInputModel.Text, answers);
            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var existingEntries = await _questionService.GetQuestionGroup(id);
            if (existingEntries == null || existingEntries.Count == 0)
                return NotFound();
            var questionText = existingEntries.First().QuestionText;
            await _questionService.UpdateQuestionGroup(questionText, string.Empty, new List<(string, bool)>());
            return RedirectToPage();
        }
    }

    public class QuestionInputModel
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}
