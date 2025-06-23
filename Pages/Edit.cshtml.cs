using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz.Models;
using Quiz.Services;
namespace Quiz.Pages;

public class EditModel : PageModel
{
    private readonly IQuizService _questionService;

    public EditModel(IQuizService questionService)
    {
        _questionService = questionService;
    }

    [BindProperty]
    public QuestionInputModel QuestionInputModel { get; set; }

    public List<QuizEntry> Questions { get; private set; }

    [BindProperty]
    public int Id { get; set; }
    

    public async Task<IActionResult> OnGet(int id)
    {
        Questions = await _questionService.GetQuestionGroup(id); ;
        if (Questions == null) return NotFound();

        QuestionInputModel = new QuestionInputModel
        {
            Text = Questions.First().QuestionText,
            Answers = Questions.Select(q => q.AnswerText).ToList(),
            CorrectOptionIndex = Questions.FindIndex(e => e.IsCorrect)
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var existingEntries = await _questionService.GetQuestionGroup(Id);
        if (existingEntries == null || existingEntries.Count == 0)
            return NotFound();

        var oldQuestionText = existingEntries.First().QuestionText;

        var answers = QuestionInputModel.Answers
            .Select((text, index) => (AnswerText: text, IsCorrect: index == QuestionInputModel.CorrectOptionIndex))
            .ToList();

        await _questionService.UpdateQuestionGroup(oldQuestionText, QuestionInputModel.Text, answers);

        return RedirectToPage("/Question");
    }
}
